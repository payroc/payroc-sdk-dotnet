using Payroc.CardPayments.Refunds;

namespace Payroc.TestFunctional.CardPayments.Refunds;

[TestFixture, Category("CardPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRefundRequest = Data.Get<UnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs  )
        ]);

        var createdRefundResponse = await client.CardPayments.Refunds.CreateUnreferencedRefundAsync(createRefundRequest);

        Assert.That(createdRefundResponse.TransactionResult.Status, Is.EqualTo(TransactionResultStatus.Ready));
    }
}
