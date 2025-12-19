using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.CardPayments.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class LifecycleTests
{
    [Test]
    public async Task Payments_Create_Retrieve_Lifecycle()
    {
        var client = GlobalFixture.Payments;
        
        // create a payment - AVS disabled
        var paymentRequest = Data.Get<PaymentRequest>([
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdNoAvs ),
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        var paymentResponse = await client.CardPayments.Payments.CreateAsync(paymentRequest);
        
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
        var paymentRequestAvs = Data.Get<PaymentRequest>([
            ( i => i.ProcessingTerminalId,  GlobalFixture.TerminalIdAvs ),
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        var paymentResponseAvs = await client.CardPayments.Payments.CreateAsync(paymentRequestAvs);
        
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
