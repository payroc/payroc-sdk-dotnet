# Payroc API .Net SDK

[![nuget shield](https://img.shields.io/nuget/v/Payroc)](https://nuget.org/packages/Payroc)

The Payroc API .Net SDK provides convenient access to the Payroc API from .Net.

## Installation

```sh
dotnet add package Payroc
```

## Usage

### API Key

You need to provide your API Key to the `PayrocClient` constructor. In this example we read it from an environment variable named `PAYROC_API_KEY`. In your own code you should consider security and compliance best practices, likely retrieving this value from a secure vault on demand.

### PayrocClient

Instantiate and use the client with the following:

```csharp
using Payroc;

var apiKey = Environment.GetEnvironmentVariable("PAYROC_API_KEY") ?? throw new Exception("Payroc API Key not found");
var client = new PayrocClient(apiKey);
```

Then you can access the various API endpoints through the `client` object. For example, to create a payment:

```csharp
using Payroc.Payments;

await client.Payments.CreateAsync(
    new PaymentRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Channel = PaymentRequestChannel.Web,
        ProcessingTerminalId = "1234001",
        Operator = "Postman",
        Order = new PaymentOrder
        {
            OrderId = "OrderRef6543",
            Description = "Large Pepperoni Pizza",
            Amount = 4999,
            Currency = Currency.Usd,
        },
        Customer = new Customer
        {
            FirstName = "Sarah",
            LastName = "Hopper",
            BillingAddress = new Address
            {
                Address1 = "1 Example Ave.",
                Address2 = "Example Address Line 2",
                Address3 = "Example Address Line 3",
                City = "Chicago",
                State = "Illinois",
                Country = "US",
                PostalCode = "60056",
            },
            ShippingAddress = new Shipping
            {
                RecipientName = "Sarah Hopper",
                Address = new Address
                {
                    Address1 = "1 Example Ave.",
                    Address2 = "Example Address Line 2",
                    Address3 = "Example Address Line 3",
                    City = "Chicago",
                    State = "Illinois",
                    Country = "US",
                    PostalCode = "60056",
                },
            },
        },
        PaymentMethod = new PaymentRequestPaymentMethod(
            new PaymentRequestPaymentMethod.Card(
                new CardPayload
                {
                    CardDetails = new CardPayloadCardDetails(
                        new CardPayloadCardDetails.Raw(
                            new RawCardDetails
                            {
                                Device = new Device
                                {
                                    Model = DeviceModel.BbposChp,
                                    SerialNumber = "PAX123456789",
                                },
                                RawData =
                                    "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
                            }
                        )
                    ),
                }
            )
        ),
        CustomFields = new List<CustomField>()
        {
            new CustomField { Name = "yourCustomField", Value = "abc123" },
        },
    }
);
```

## Exception Handling

When the API returns a non-success status code (4xx or 5xx response), a subclass of the following error will be thrown.

```csharp
using Payroc;

try
{
    var response = await client.Payments.CreateAsync(...);
}
catch (PayrocApiException e)
{
    System.Console.WriteLine(e.Body);
    System.Console.WriteLine(e.StatusCode);
}
```

## Logging

> [!WARNING]  
> Be careful when configuring your logging not to log the headers of outbound HTTP requests, lest you leak an API key or access token.

## Pagination

List endpoints are paginated. The SDK provides an async enumerable so that you can simply loop over the items:

```csharp
using Payroc.Payments;
using Payroc;

var apiKey = Environment.GetEnvironmentVariable("PAYROC_API_KEY")
    ?? throw new Exception("Payroc API Key not found");
var client = new PayrocClient(apiKey);
var pager = await client.Payments.ListAsync(
    new ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        OrderId = "OrderRef6543",
        Operator = "Jane",
        CardholderName = "Sarah%20Hazel%20Hopper",
        First6 = "453985",
        Last4 = "7062",
        DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000),
        DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000),
        SettlementDate = "2024-07-02",
        Before = "2571",
        After = "8516",
    }
);

await foreach (var item in pager)
{
    // do something with item
}
```

## Polymorphic Types

Our API makes frequent use of polymorphic data structures. This is when a value might be one of multiple types, and the type is determined at runtime. For example, a contact method can be one of several methods, such as `Email` or `Fax`. The SDK provides a way to handle this using the `OneOf` library, as well as some helper methods.

### Creating Polymorphic Data

Normally, when something has a single type, we simply use the constructor. We can also use var to avoid specifying the receiving type. For example:

```csharp
var address = new Address()
{
    // ...
};
```

Since C# 9 / .Net 5, we can use "target-typed expressions" to simplify it as just `new()`, omitting the name of the `Address` type, albeit specifying `Address` at the beginning to specify the type:

```csharp
Address address = new()
{
    // ...
};
```

However, when dealing with polymorphic types, we need to specify which type we are using, so can no longer use "target-typed expressions". We provide a helper factory method on each polymorphic type to help achieve this cleanly.

The `ContactMethod` type has a method for each possible polymorphic variant, such as `ContactMethod.Email()`, `ContactMethod.Fax()` etc.

This can look as simple as:

```csharp
var a = new ContactMethod.Email(new() { Value = "jane.doe@example.com" });
var b = new ContactMethod.Fax(new() { Value = "2025550110" });
```

Note the use of both `var` and the `new()` expression to create the inner object, which is a `ContactMethodEmail` in this case. This is a common pattern in the SDK to keep the code clean and readable.

So to summarize, when you're looking to create a polymorpohic instance, you should start by locating the factory method for the type you're after, and then in that constructor, can use the "target-typed expression" to automatically select the correct inner variant type for you. This pattern will keep your code clean from unnecessary boilerplate / noise.

### Handling Polymorphic Data

Let's look at an example of how we can interact with polymorphic types in SDK responses:

```csharp

var owners = await client.Boarding.Owners.RetrieveAsync(new() { OwnerId = 4564 });

foreach (var contactMethod in owners.ContactMethods)
{
    // How to read common properties regardless of type
    Console.WriteLine($"Contact Method: {contactMethod.Type} - {contactMethod.Value}");

    // How to check if the contact method is a particular type, and then extract the specific, inner type
    if (contactMethod.IsEmail)
    {
        var email = contactMethod.AsEmail();

        // If email had type-specific properties, you could access them on this instance, e.g. email.SomeTypeSpecificProperty
    }

    // How to use the `Match ()` function to handle different types:
    var valueOnlyIfItIsPhoneOrMobile = contactMethod.Match(
        onEmail: email => $"Email: {email.Value}",
        onPhone: phone => $"Phone: {phone.Value}",
        onMobile: mobile => $"Mobile: {mobile.Value}",
        onFax: fax => $"Fax: {fax.Value}",
        onUnknown_: (typeName, value) => "Unknown contact method type"
    );

    // How to use `Visit()` to apply different actions based on different types:
    contactMethod.Visit(
        onEmail: email => someService.SendWelcomeEmail(email.Value),
        onPhone: phone => { },
        onMobile: mobile => { },
        onFax: fax => someService.SendWelcomeFax(fax.Value),
        onUnknown_: (typeName, value) => { }
    );
```

## Advanced

### Timeouts

The SDK defaults to a 30 second timeout. Use the `Timeout` option to configure this behavior.

```csharp
var response = await client.Payments.CreateAsync(
    ...,
    new RequestOptions
    {
        Timeout: TimeSpan.FromSeconds(3) // Override timeout to 3s
    }
);
```

## References

The Payroc API SDK is generated via [Fern](https://www.buildwithfern.com/).

[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-Built%20with%20Fern-brightgreen)](https://buildwithfern.com?utm_source=github&utm_medium=github&utm_campaign=readme&utm_source=https%3A%2F%2Fgithub.com%2Fpayroc%2Fpayroc-sdk-dotnet)
