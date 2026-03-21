using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestHarness.Factory;

public static class TokenizationRequestFactory
{
    public static TokenizationRequest Create(string processingTerminalId)
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            ProcessingTerminalId = processingTerminalId,
            Operator = "Jane",
            MitAgreement = TokenizationRequestMitAgreement.Unscheduled,
            Source = new TokenizationRequestSource.Card(new CardPayload
            {
                CardDetails = new CardPayloadCardDetails.Keyed(new KeyedCardDetails
                {
                    KeyedData = new KeyedCardDetailsKeyedData.PlainText(new PlainTextKeyedDataFormat
                    {
                        CardNumber = "4539858876047062",
                        ExpiryDate = "1230",
                        Cvv = "234",
                    }),
                    CardholderName = "Sarah Hazel Hopper",
                }),
            }),
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
                ContactMethods = new List<ContactMethod>
                {
                    new ContactMethod.Email(new() { Value = "sarah.hopper@example.com" }),
                },
                NotificationLanguage = CustomerNotificationLanguage.En,
            },
            IpAddress = new IpAddress
            {
                Type = IpAddressType.Ipv4,
                Value = "104.18.24.203",
            },
            CustomFields = new List<CustomField>
            {
                new CustomField { Name = "yourCustomField", Value = "abc123" },
            },
        };
}
