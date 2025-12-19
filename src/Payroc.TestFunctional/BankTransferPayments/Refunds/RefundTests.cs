using Payroc.BankTransferPayments.Payments;
using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestFunctional.Payments.BankTransferPayments;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
[Ignore("Data Errors: Processing terminal id does not support bank transfers.")]
public class RefundTests
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
        var refundRequest = Data.Get<BankTransferReferencedRefund>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.PaymentId, createResponse.PaymentId ),
            ]);

        var refundResponse = await client.BankTransferPayments.Refunds.RefundAsync(refundRequest);

        Assert.That(refundResponse.PaymentId, Is.Not.Null);
    }
}
