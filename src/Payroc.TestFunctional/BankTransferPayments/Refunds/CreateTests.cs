using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestFunctional.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    [Ignore("Data Errors: Processing terminal id does not support bank transfers.")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<BankTransferUnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);

        try
        {
            var response = await client.BankTransferPayments.Refunds.CreateAsync(request);
            Assert.That(response.TransactionResult.Status, Is.EqualTo("ready"));
            Assert.That(response.RefundId, Is.Not.Null);
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"Exception occurred: {ex.Message}");
        }
    }
}
