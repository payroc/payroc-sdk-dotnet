using Payroc.Payments;

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
            PaymentMethod = new(new PaymentRequestPaymentMethod.Card(new CardPayload
            {
                CardDetails = new CardPayloadCardDetails.Keyed(new KeyedCardDetails() 
                {
                    KeyedData = new KeyedCardDetailsKeyedData.PlainText( new PlainTextKeyedDataFormat()
                    {
                        Device = new()
                        {
                            Model = DeviceModel.PaxA80,
                            SerialNumber = "WPC202833004712"
                        },
                        ExpiryDate = "1225",
                        CardNumber = "4539858876047062"
                    })
                })
            }))
        };
}
