using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestCommon.Tests.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    [Ignore("No ACH-capable terminal available - need bank transfer terminal config")]
    public async Task BankTransferRefunds_Retrieve_Success()
    {
        var client = GlobalFixture.Payments; 
        var refundRequest = Data.Get<BankTransferUnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBank ),
        ]);
        var refundResponse = await client.BankTransferPayments.Refunds.CreateAsync(refundRequest);
        var retrieveRequest = new RetrieveRefundsRequest
        {
            RefundId = refundResponse.RefundId
        };
        var retrieveResponse = await client.BankTransferPayments.Refunds.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.RefundId, Is.EqualTo(refundResponse.RefundId));
    }
}
