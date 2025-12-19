using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestFunctional.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
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
        var refundResponse = await client.BankTransferPayments.Refunds.CreateAsync(refundRequest);
        var retrieveRequest = new RetrieveRefundsRequest()
        {
            RefundId = refundResponse.RefundId
        };
        var retrieveResponse = await client.BankTransferPayments.Refunds.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.RefundId, Is.EqualTo(refundResponse.RefundId));
    }
}
