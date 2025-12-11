using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestFunctional.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
{
    [Test]
    [Ignore("Data Errors: Processing terminal id does not support bank transfers.")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<BankTransferUnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs ),
        ]);
        _ = await client.BankTransferPayments.Refunds.CreateAsync(request);
        _ = await client.BankTransferPayments.Refunds.CreateAsync(request);
        _ = await client.BankTransferPayments.Refunds.CreateAsync(request);
        var listRequest = new ListRefundsRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
        };
        
        var listResponse = await client.BankTransferPayments.Refunds.ListAsync(listRequest);
        
        Assert.That(listResponse.CurrentPage.Count(), Is.GreaterThan(1));
    }
}
