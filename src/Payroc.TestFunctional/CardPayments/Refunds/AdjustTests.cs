using Payroc.CardPayments.Refunds;

namespace Payroc.TestFunctional.CardPayments.Refunds;

[TestFixture, Category("CardPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class AdjustTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRefundRequest = Data.Get<UnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        var createdRefundResponse = await client.CardPayments.Refunds.CreateUnreferencedRefundAsync(createRefundRequest);
        var adjustRefundRequest = Data.Get<RefundAdjustment>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.RefundId, createdRefundResponse.RefundId )
        ]);

        var adjustedRefundResponse = await client.CardPayments.Refunds.AdjustAsync(adjustRefundRequest);

        Assert.That(adjustedRefundResponse.TransactionResult.Status, Is.EqualTo(TransactionResultStatus.Ready));
    }
}
