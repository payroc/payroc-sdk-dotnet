# Payroc API .Net SDK

The Payroc API .Net SDK provides convenient access to the Payroc API from .Net.

## Contents

- [Payroc API .Net SDK](#payroc-api-net-sdk)
  - [Installation](#installation)
  - [Usage](#usage)
    - [API Key](#api-key)
    - [PayrocClient](#payrocclient)
      - [Advanced Usage with Custom Environment](#advanced-usage-with-custom-environment)
  - [Exception Handling](#exception-handling)
  - [Logging](#logging)
  - [Pagination](#pagination)
    - [Pagination Gotcha](#pagination-gotcha)
  - [Request Parameters](#request-parameters)
  - [Polymorphic Types](#polymorphic-types)
    - [Creating Polymorphic Data](#creating-polymorphic-data)
    - [Handling Polymorphic Data](#handling-polymorphic-data)
  - [Advanced](#advanced)
    - [Timeouts](#timeouts)
    - [Error Telemetry](#error-telemetry)
      - [Opt-Out](#opt-out)
      - [Privacy](#privacy)
  - [Contributing](#contributing)
  - [References](#references)

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
using Payroc.CardPayments.Payments;

await client.CardPayments.Payments.CreateAsync(
    new PaymentRequest
    {
        IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
        Channel = PaymentRequestChannel.Web,
        ProcessingTerminalId = "1234001",
        Operator = "Postman",
        Order = new PaymentOrderRequest
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

### Advanced Usage with Custom Environment

If you wish to use the SDK against a custom URL, such as a mock API server, you can provide a custom `PayrocEnvironment` to the `PayrocClient` constructor:

```csharp
var mockEnvironment = new PayrocEnvironment
{
    Api = "http://localhost:3000",
    Identity = "http://localhost:3001"
};

var client = new PayrocClient(
    apiKey,
    new ClientOptions
    {
        Environment = mockEnvironment
    }
);
```

## Exception Handling

When the API returns a non-success status code (4xx or 5xx response), a subclass of the following error will be thrown.

```csharp
using Payroc;

try
{
    var response = await client.CardPayments.Payments.CreateAsync(...);
}
catch (PayrocApiException e)
{
    System.Console.WriteLine(e.Body);
    System.Console.WriteLine(e.StatusCode);
}
```

Here are the specific exceptions:

- `BadRequestError`
- `ConflictError`
- `ContentTooLargeError`
- `ForbiddenError`
- `InternalServerError`
- `NotAcceptableError`
- `NotFoundError`
- `UnauthorizedError`
- `UnsupportedMediaTypeError`

Catching a specific exception will allow reading the specific data structure, and easier reading of messages. For example:

```csharp
using Payroc;

try
{
    var response = await client.CardPayments.Payments.CreateAsync(...);
}
catch (BadRequestError e)
{
    // Specific handling of a specific error, `BadRequestError`, allows reading more detail
    // Details has the overall message, e.g. "Validation error..."
    System.Console.WriteLine(e.Body.Details);
    
    foreach(var error in e.Body.Errors)    
    {
        // These individual messages will list details, e.g. individual validation errors
        System.Console.WriteLine(error.Message);
    }

    System.Console.WriteLine(e.StatusCode);
}
catch (PayrocApiException e)
{
    // Fallback to generic exception
    // Note there's no `.Details` on `e.Body`
    System.Console.WriteLine(e.Body);
    System.Console.WriteLine(e.StatusCode);
}
```

## Logging

> [!WARNING]  
> Be careful when configuring your logging not to log the headers of outbound HTTP requests, lest you leak an API key or access token.

## Pagination

List endpoints are paginated. The SDK provides an `IAsyncEnumerable` so that you can simply loop over the items. Note the `await` before the `foreach`:

```csharp
using Payroc.CardPayments.Payments;
using Payroc;

var pager = await client.CardPayments.Payments.ListAsync(new ListPaymentsRequest { ProcessingTerminalId = "1234001"});

await foreach (var item in pager)
{
    // do something with item
}
```

### Pagination Gotcha

Beware of iterating the items on a single page and thinking that they are all there are. In the following example, there are only 10 items of the available 100, because this is iterating the items on a single page:

```csharp
var pager = await client.CardPayments.Payments.ListAsync(new ListPaymentsRequest());

var ids = new List<int>();

foreach (var payment in pager.CurrentPage.Items)
{
    var id = payment.PaymentId;
    ids.Add(int.Parse(id[^2..]));
}
```

This might be helpful when you only want to process the first few results, but to iterate all items, the `await foreach` approach is recommended.

## Request Parameters

Sometimes you need to filter results, for example, retrieving results from a given date. Raw API calls might use query parameters. The SDK equivalent pattern is setting the values in the request object itself.

Examples of setting different query parameters via the request object:

```csharp
    new ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000)
    }
```

```csharp
    new ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000)
    }
```

```csharp
    new ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        After = "8516"
    }
```

```csharp
    new ListPaymentsRequest
    {
        ProcessingTerminalId = "1234001",
        Before = "2571"
    }
```

Inspect the code definition of your particular `...Request` object in your IDE to see what properties can be used for filtering.

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
using Payroc.Boarding.Owners;

var owners = await client.Boarding.Owners.RetrieveAsync(new RetrieveOwnersRequest { OwnerId = 4564 });

foreach (var contactMethod in owners.ContactMethods)
{
    // How to read common properties regardless of type
    Console.WriteLine($"Contact Method: {contactMethod.Type} - {contactMethod.Value}");

    // How to check if the contact method is a particular type,
    // and then extract the specific, inner type
    if (contactMethod.IsEmail)
    {
        var email = contactMethod.AsEmail();

        // If email had type-specific properties, you could access them on this instance,
        // e.g. email.SomeTypeSpecificProperty
    }

    // How to use the `Match ()` function to handle different types:
    var formattedValue = contactMethod.Match(
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

The SDK defaults to a 30 second timeout. You can configure this with a timeout option at the client or request level.

```csharp
// Client level
var client = new PayrocClient(
    apiKey,
    new ClientOptions
    {
        Timeout = TimeSpan.FromSeconds(10)
    }
);

// Request level
var response = await client.CardPayments.Payments.CreateAsync(
    ...,
    new RequestOptions
    {
        Timeout = TimeSpan.FromSeconds(3) // Override timeout to 3s
    }
);
```

### Error Telemetry

The SDK automatically reports anonymous errors to help improve the SDK quality. This is enabled by default but can be disabled.

#### Opt-Out

To disable error telemetry:

```csharp
var client = new PayrocClient(
    apiKey,
    new ClientOptions
    {
        Telemetry = false
    }
);
```

#### Privacy

All sensitive data (API keys, tokens, passwords, PII) is automatically scrubbed before transmission.

## Contributing

While we value open-source contributions to this SDK, this library is generated programmatically.
Additions made directly to this library would have to be moved over to our generation code,
otherwise they would be overwritten upon the next generated release. Feel free to open a PR as
a proof of concept, but know that we will not be able to merge it as-is. We suggest opening
an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!

For details on setting up your development environment, running tests, and code quality standards, please see [CONTRIBUTING.md](./CONTRIBUTING.md).

## References

The Payroc API SDK is generated via [Fern](https://www.buildwithfern.com/).

[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-Built%20with%20Fern-brightgreen)](https://buildwithfern.com?utm_source=github&utm_medium=github&utm_campaign=readme&utm_source=https%3A%2F%2Fgithub.com%2Fpayroc%2Fpayroc-sdk-dotnet)
