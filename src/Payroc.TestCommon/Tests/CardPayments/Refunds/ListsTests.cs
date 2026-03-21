using Payroc.CardPayments.Refunds;

namespace Payroc.TestCommon.Tests.CardPayments.Refunds;

[TestFixture, Category("CardPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListsTests
{
    [Test]
    public async Task CardPaymentRefunds_Lists_Success()
    {
        var client = GlobalFixture.Payments;
        var createRefundRequest = Data.Get<UnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs  )
        ]);
        _ = await client.CardPayments.Refunds.CreateUnreferencedRefundAsync(createRefundRequest);
        _ = await client.CardPayments.Refunds.CreateUnreferencedRefundAsync(createRefundRequest);
        var listRefundsRequest = new ListRefundsRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            Operator = "Jane"
        };

        var listRefundsResponse = await client.CardPayments.Refunds.ListAsync(listRefundsRequest);

        Assert.That(listRefundsResponse.CurrentPage.Count, Is.GreaterThanOrEqualTo(2));
    }
}
