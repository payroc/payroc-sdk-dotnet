using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class LifecycleTests
{
    private static string NewKey() => Guid.NewGuid().ToString();

    [Test]
#if !WINDOWS
    [Ignore("Windows only")]
#endif
    public async Task Payments_Create_Retrieve_Lifecycle()
    {
        var client = GlobalFixture.Payments;
        
        // create a payment - AVS disabled
        PlainTextKeyedDataFormat DataFormat;
        var paymentResponse = await client.CardPayments.Payments.CreateAsync(
            new PaymentRequest
            {
                
                IdempotencyKey = NewKey(),
                Channel = PaymentRequestChannel.Web,
                ProcessingTerminalId = GlobalFixture.TerminalIdNoAvs,
                Operator = "SDK Test",
    
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
                                new CardPayloadCardDetails.Keyed(
                                    new KeyedCardDetails
                                    {
                                        CardholderName = "SDK Test",
                                        KeyedData = new KeyedCardDetailsKeyedData(
                                            DataFormat = new PlainTextKeyedDataFormat
                                            {
                                                CardNumber = "4111111111111111",
                                                ExpiryDate = "1225"
                                            })
                                    }))
                        }))
            });
        
        // retrieve the payment
        var paymentDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
            new RetrievePaymentsRequest
            {
                PaymentId = paymentResponse.PaymentId
            });
        
        Assert.Multiple(() =>
        {
            Assert.That(paymentDetailResponse.TransactionResult.Status, Is.EqualTo("ready"));
            Assert.That(paymentDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
            Assert.That(paymentDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("A"));
            Assert.That(paymentDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
        });
        
        // create a payment - AVS enabled
        var paymentResponseAvs = await client.CardPayments.Payments.CreateAsync(
            new PaymentRequest
            {
                
                IdempotencyKey = NewKey(),
                Channel = PaymentRequestChannel.Web,
                ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
                Operator = "SDK Test",
    
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
                                new CardPayloadCardDetails.Keyed(
                                    new KeyedCardDetails
                                    {
                                        CardholderName = "SDK Test",
                                        KeyedData = new KeyedCardDetailsKeyedData(
                                            DataFormat = new PlainTextKeyedDataFormat
                                            {
                                                CardNumber = "4111111111111111",
                                                ExpiryDate = "1225"
                                            })
                                    }))
                        }))
            });
        
        // retrieve the payment
        var paymentDetailResponseAvs = await client.CardPayments.Payments.RetrieveAsync(
            new RetrievePaymentsRequest
            {
                PaymentId = paymentResponseAvs.PaymentId
            });
        
        Assert.Multiple(() =>
        {
            Assert.That(paymentDetailResponseAvs.TransactionResult.Status, Is.EqualTo("ready"));
            Assert.That(paymentDetailResponseAvs.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
            Assert.That(paymentDetailResponseAvs.TransactionResult.ResponseCode, Is.EqualTo("A"));
            Assert.That(paymentDetailResponseAvs.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
        });
    }
}
