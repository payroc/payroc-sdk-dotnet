using Payroc.BankTransferPayments.Payments;

namespace Payroc.TestFunctional.BankTransferPayments.Payments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
[Ignore("Data Errors: Processing terminal id does not support bank transfers.")]
public class ListTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs ),
        ]); 
        _ = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        _ = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        var ListRequest = new ListPaymentsRequest()
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
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
