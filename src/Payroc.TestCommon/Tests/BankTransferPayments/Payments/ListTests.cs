using Payroc.BankTransferPayments.Payments;

namespace Payroc.TestCommon.Tests.BankTransferPayments.Payments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
{
    [Test]
    public async Task BankTransferPayments_List_Success()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad ),
        ]); 
        createRequest.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
        _ = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        _ = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        var ListRequest = new ListPaymentsRequest()
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdBankPad
        };
        
        try
        {
            var listResponse = await client.BankTransferPayments.Payments.ListAsync(ListRequest);
            Assert.That(listResponse.CurrentPage.Count(), Is.GreaterThan(1));
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"Exception occurred: {ex.Message}");
        }
    }
}
