using Payroc.BankTransferPayments.Payments;

namespace Payroc.TestFunctional.Payments.BankTransferPayments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
[Ignore("Data Errors: Processing terminal id does not support bank transfers.")]
public class RepresentTests
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
        var createResponse = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        var representRequest = Data.Get<Representment>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.PaymentId, createResponse.PaymentId ),
            ]);
        
        try
        {
            var representResponse = await client.BankTransferPayments.Payments.RepresentAsync(representRequest);
            Assert.That(representResponse.PaymentId, Is.Not.Null);
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
