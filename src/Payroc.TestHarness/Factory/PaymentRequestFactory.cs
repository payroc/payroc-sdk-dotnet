using Payroc.CardPayments.Payments;

namespace Payroc.TestHarness.Factory;

public class PaymentRequestFactory
{
    public static PaymentRequest Create(string processingTerminalId = "5984001")
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            Channel = PaymentRequestChannel.Web,
            ProcessingTerminalId = processingTerminalId,
            Operator = "Jane",
            Order = new()
            {
                OrderId = ("Order" + Guid.NewGuid().ToString())[..23],
                Description = "Large Pepperoni Pizza",
                Currency = Currency.Usd,
                Amount = 4999
            },
            Customer = new()
            {
                FirstName = "Sarah",
                LastName = "Hopper",
                BillingAddress = new()
                {
                    Address1 = "1 Example Ave.",
                    Address2 = "Example Address Line 2",
                    Address3 = "Example Address Line 3",
                    City = "Chicago",
                    State = "Illinois",
                    Country = "US",
                    PostalCode = "60056"
                },
                ShippingAddress = new()
                {
                    RecipientName = "Sarah Hopper",
                    Address = new()
                    {
                        Address1 = "1 Example Ave.",
                        Address2 = "Example Address Line 2",
                        Address3 = "Example Address Line 3",
                        City = "Chicago",
                        State = "Illinois",
                        Country = "US",
                        PostalCode = "60056"
                    }
                }
            },
            PaymentMethod = new(new PaymentRequestPaymentMethod.Card(CardPayloadFactory.Create()))
        };
}
