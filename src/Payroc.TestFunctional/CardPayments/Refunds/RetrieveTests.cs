using Payroc.CardPayments.Refunds;

namespace Payroc.TestFunctional.Payments.Refunds;

[TestFixture, Category("CardPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRefundRequest = Data.Get<UnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs)
        ]);
        var createdRefundResponse = await client.CardPayments.Refunds.CreateUnreferencedRefundAsync(createRefundRequest);
        var retrieveRefundRequest = new RetrieveRefundsRequest
        {
            RefundId = createdRefundResponse.RefundId
        };

        var retrievedRefundResponse = await client.CardPayments.Refunds.RetrieveAsync(retrieveRefundRequest);

        Assert.That(retrievedRefundResponse.TransactionResult.Status, Is.EqualTo(TransactionResultStatus.Ready));
    }
}
