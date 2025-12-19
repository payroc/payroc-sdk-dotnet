using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.CardPayments.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class DeclineTests
{
    private static string NewKey() => Guid.NewGuid().ToString();

    [Test]
#if !WINDOWS
    [Ignore("Windows only")]
#endif
    public async Task Payments_Create_Retrieve_Decline()
    {
        var client = GlobalFixture.Payments;
        
       // create a payment - should decline
        PlainTextKeyedDataFormat DataFormat;
        var paymentDeclineResponse = await client.CardPayments.Payments.CreateAsync(
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
                    Amount = 4901,
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
        var paymentDeclineDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
            new RetrievePaymentsRequest
            {
                PaymentId = paymentDeclineResponse.PaymentId
            });
        
        Assert.Multiple(() =>
        {
            Assert.That(paymentDeclineDetailResponse.TransactionResult.Status, Is.EqualTo("declined"));
            Assert.That(paymentDeclineDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("Do Not Honor"));
            Assert.That(paymentDeclineDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("D"));
            Assert.That(paymentDeclineDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("05"));
        });
    }
    
    [Test]
#if !WINDOWS
    [Ignore("Windows only")]
#endif
    public async Task Payments_Create_Retrieve_Referral()
    {
        var client = GlobalFixture.Payments;
        
        // should be referral
        PlainTextKeyedDataFormat DataFormat;
        var paymentReferralResponse = await client.CardPayments.Payments.CreateAsync(
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
                    Amount = 4902,
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
        var paymentReferralDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
            new RetrievePaymentsRequest
            {
                PaymentId = paymentReferralResponse.PaymentId
            });
        
        Assert.Multiple(() =>
        {
            Assert.That(paymentReferralDetailResponse.TransactionResult.Status, Is.EqualTo("referral"));
            Assert.That(paymentReferralDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("Refer to Card Issuer"));
            Assert.That(paymentReferralDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("R"));
            Assert.That(paymentReferralDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("01"));
        });
    }
    
    [Test]
    [Retry(3)]
    [Category("Payments")]
#if !WINDOWS
    [Ignore("Windows only")]
#endif
    public async Task Payments_Create_Cvv_Fail()
    {
        var client = GlobalFixture.Payments; 
        
        // should be cvv fail
        PlainTextKeyedDataFormat DataFormat;
        var paymentCvvFailResponse = await client.CardPayments.Payments.CreateAsync(
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
                    Amount = 1403,
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
        var paymentCvvFailDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
            new RetrievePaymentsRequest
            {
                PaymentId = paymentCvvFailResponse.PaymentId
            });
        
        Assert.Multiple(() =>
        {
            Assert.That(paymentCvvFailDetailResponse.TransactionResult.Status, Is.EqualTo("declined"));
            Assert.That(paymentCvvFailDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("AVS FAILURE"));
            Assert.That(paymentCvvFailDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("D"));
            Assert.That(paymentCvvFailDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
        });
    }
}
