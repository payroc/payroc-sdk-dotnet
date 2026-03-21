using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestCommon.Tests.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
{
    [Test]
    [Ignore("No ACH-capable terminal available - need bank transfer terminal config")]
    public async Task BankTransferRefunds_List_Success()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<BankTransferUnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBank )
        ]);
        _ = await client.BankTransferPayments.Refunds.CreateAsync(request);
        _ = await client.BankTransferPayments.Refunds.CreateAsync(request);
        _ = await client.BankTransferPayments.Refunds.CreateAsync(request);
        var listRequest = new ListRefundsRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdBank
        };
        
        var listResponse = await client.BankTransferPayments.Refunds.ListAsync(listRequest);
        
        Assert.That(listResponse.CurrentPage.Count(), Is.GreaterThan(2));
    }
}
