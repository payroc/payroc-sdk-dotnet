using Payroc.CardPayments.Refunds;

namespace Payroc.TestFunctional.Payments.Refunds;

[TestFixture, Category("CardPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ReverseRefundTests
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
        var reverseRefundRequest = Data.Get<ReverseRefundRefundsRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.RefundId, createdRefundResponse.RefundId )
        ]);

        var reversedRefundResponse = await client.CardPayments.Refunds.ReverseRefundAsync(reverseRefundRequest);

        Assert.That(reversedRefundResponse.TransactionResult.Status, Is.EqualTo(TransactionResultStatus.Reversal));
    }
}
