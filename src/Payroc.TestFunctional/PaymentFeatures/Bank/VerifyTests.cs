using Payroc.PaymentFeatures.Bank;

namespace Payroc.TestFunctional.Payments.BankAccounts;

[TestFixture, Category("Payments.BankAccounts")]
[Parallelizable(ParallelScope.Fixtures)]
public class VerifyTests
{
    [Test]
    [Ignore("Data Errors: Processing terminal does not support bank transfers.")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<BankAccountVerificationRequest>([
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs)
        ]);
        
        try
        {
            var response = await client.PaymentFeatures.Bank.VerifyAsync(request);
            Assert.That(response.Verified, Is.True);
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"Exception occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception occurred: {ex.Message}");
        }
    }
}