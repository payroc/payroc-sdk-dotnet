using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestFunctional.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ReverseRefundTests
{
    [Test]
    [Ignore("Data Errors: Processing terminal id does not support bank transfers.")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var refundRequest = Data.Get<BankTransferUnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs ),
        ]);
        var createRefundResponse = await client.BankTransferPayments.Refunds.CreateAsync(refundRequest);
        var reverseRequest = Data.Get<ReverseRefundRefundsRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.RefundId, createRefundResponse.RefundId ),
            ]);

        var reverseResponse = await client.BankTransferPayments.Refunds.ReverseRefundAsync(reverseRequest);

        Assert.That(reverseResponse.RefundId, Is.Not.Null);
        Assert.That(reverseResponse.TransactionResult.Status, Is.EqualTo("reversal"));
    }
}
