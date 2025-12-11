

using Payroc.BankTransferPayments.Payments;

namespace Payroc.TestFunctional.Payments.BankTransferPayments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
[Ignore("Data Errors: Processing terminal id does not support bank transfers.")]
public class RetrieveTests
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
        var retrieveRequest = new RetrievePaymentsRequest()
        {
            PaymentId = createResponse.PaymentId,
        };
        
        var retrieveResponse = await client.BankTransferPayments.Payments.RetrieveAsync(retrieveRequest);

        Assert.That(retrieveResponse.PaymentId, Is.EqualTo(createResponse.PaymentId));
    }
}