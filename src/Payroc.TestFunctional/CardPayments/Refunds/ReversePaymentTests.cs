using Payroc.CardPayments.Payments;
using Payroc.CardPayments.Refunds;

namespace Payroc.TestFunctional.CardPayments.Refunds;

[TestFixture, Category("CardPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ReversePaymentTests
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
        var request = Data.Get<PaymentReversal>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.PaymentId, createPaymentResponse.PaymentId ),
            ]);

        var response = await client.CardPayments.Refunds.ReverseAsync(request);

        Assert.That(response.PaymentId, Is.Not.Null);
    }
}
