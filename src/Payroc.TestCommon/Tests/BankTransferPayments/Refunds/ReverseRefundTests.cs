using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestCommon.Tests.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ReverseRefundTests
{
    [Test]
    [Ignore("Data Errors: ReverseRefunds will not work with a transaction with a declined status.")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.PaymentsBank;
        var refundRequest = Data.Get<BankTransferUnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad )
        ]);
        var createRefundResponse = await client.BankTransferPayments.Refunds.CreateAsync(refundRequest);
        var reverseRequest = Data.Get<ReverseRefundRefundsRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.RefundId, createRefundResponse.RefundId ),
            ]);

        var reverseResponse = await client.BankTransferPayments.Refunds.ReverseRefundAsync(reverseRequest);

        Assert.That(reverseResponse.RefundId, Is.Not.Null);
    }
}
