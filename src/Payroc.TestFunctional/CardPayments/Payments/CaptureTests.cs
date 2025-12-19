using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.CardPayments.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class CaptureTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var paymentRequest = Data.Get<PaymentRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs ),
            ]);
        var createPaymentResponse = await client.CardPayments.Payments.CreateAsync(paymentRequest);
        var request = Data.Get<PaymentCapture>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.PaymentId, createPaymentResponse.PaymentId ),
            ]);

        var response = await client.CardPayments.Payments.CaptureAsync(request);

        Assert.That(response.PaymentId, Is.Not.Null);
    }
}
