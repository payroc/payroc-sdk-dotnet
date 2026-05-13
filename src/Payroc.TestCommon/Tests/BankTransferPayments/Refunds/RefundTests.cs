using Payroc;
using Payroc.BankTransferPayments.Payments;
using Payroc.BankTransferPayments.Refunds;
using Payroc.TestCommon.HelperMethods.BankTransfer;

namespace Payroc.TestCommon.Tests.Payments.BankTransferPayments;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class RefundTests
{
    [Test]
    public async Task BankTransferRefunds_Refund_Success()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad ),
        ]);
        createRequest.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
        createRequest.Order.Currency = Currency.Cad;
        createRequest.Order.Breakdown = null;
        createRequest.CredentialOnFile = null;
        createRequest.CustomFields = null;
        createRequest.PaymentMethod = PadPaymentMethodFactory.Create();
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
