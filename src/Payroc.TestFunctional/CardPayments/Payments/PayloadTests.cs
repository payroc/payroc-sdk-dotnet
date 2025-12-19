using Payroc.CardPayments.Payments;
using Payroc.TestFunctional.Factories.CardPayments.Payments;

namespace Payroc.TestFunctional.CardPayments.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class PayloadTests
{
    [Test]
    public async Task Payments_ICC_Payload()
    {
        var client = GlobalFixture.Payments;

            // create a payment - AVS disabled
            var paymentIccRequest = Data.Get<PaymentRequest>([
                ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdNoAvs ),
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.PaymentMethod, PayLoadFactory.CreateIcc() )
            ]);
            var paymentIccResponse = await client.CardPayments.Payments.CreateAsync(paymentIccRequest);
            
            // retrieve the payment
            var paymentIccDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
                new RetrievePaymentsRequest
                {
                    PaymentId = paymentIccResponse.PaymentId
                });
        
            Assert.Multiple(() =>
            {
                Assert.That(paymentIccDetailResponse.TransactionResult.Status, Is.EqualTo("ready"));
                Assert.That(paymentIccDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
                Assert.That(paymentIccDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("A"));
                Assert.That(paymentIccDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
            });
    }
    
    [Test]
    [Retry(3)]
    [Category("CardPayments.Payments")]
    public async Task Payments_Raw_Payload()
    {
        var client = GlobalFixture.Payments;

            // create a payment - AVS disabled
            var paymentRawRequest = Data.Get<PaymentRequest>([
                ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdNoAvs ),
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.PaymentMethod, PayLoadFactory.CreateRaw() )
            ]);
            var paymentRawResponse = await client.CardPayments.Payments.CreateAsync(paymentRawRequest);
            
            // retrieve the payment
            var paymentRawDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
                new RetrievePaymentsRequest
                {
                    PaymentId = paymentRawResponse.PaymentId
                });
        
            Assert.Multiple(() =>
            {
                Assert.That(paymentRawDetailResponse.TransactionResult.Status, Is.EqualTo("ready"));
                Assert.That(paymentRawDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
                Assert.That(paymentRawDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("A"));
                Assert.That(paymentRawDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
            });
    }
    
    [Test]
    [Retry(3)]
    [Category("CardPayments.Payments")]
    public async Task Payments_Swiped_Payload()
    { 
        var client = GlobalFixture.Payments;
        
        // create a payment - AVS disabled
        var paymentSwipedRequest = Data.Get<PaymentRequest>([
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdNoAvs ),
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.PaymentMethod, PayLoadFactory.CreateSwiped() )
        ]);
        var paymentSwipedResponse = await client.CardPayments.Payments.CreateAsync(paymentSwipedRequest);
          
        // retrieve the payment
        var paymentSwipedDetailResponse = await client.CardPayments.Payments.RetrieveAsync(
            new RetrievePaymentsRequest
            { 
                PaymentId = paymentSwipedResponse.PaymentId
            });
        
        Assert.Multiple(() =>
        {
            Assert.That(paymentSwipedDetailResponse.TransactionResult.Status, Is.EqualTo("ready"));
            Assert.That(paymentSwipedDetailResponse.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
            Assert.That(paymentSwipedDetailResponse.TransactionResult.ResponseCode, Is.EqualTo("A"));
            Assert.That(paymentSwipedDetailResponse.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
        });
    }
}
