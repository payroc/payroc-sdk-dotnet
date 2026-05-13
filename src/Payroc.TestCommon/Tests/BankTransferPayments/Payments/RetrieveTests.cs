

using Payroc;
using Payroc.BankTransferPayments.Payments;
using Payroc.TestCommon.HelperMethods.BankTransfer;

namespace Payroc.TestCommon.Tests.BankTransferPayments.Payments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task BankTransferPayments_Retrieve_Success()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad )
        ]);
        createRequest.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
        createRequest.Order.Currency = Currency.Cad;
        createRequest.Order.Breakdown = null;
        createRequest.CredentialOnFile = null;
        createRequest.CustomFields = null;
        createRequest.PaymentMethod = PadPaymentMethodFactory.Create();
        var createResponse = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        var retrieveRequest = new RetrievePaymentsRequest
        {
            PaymentId = createResponse.PaymentId,
        };
        
        var retrieveResponse = await client.BankTransferPayments.Payments.RetrieveAsync(retrieveRequest);

        Assert.That(retrieveResponse.PaymentId, Is.EqualTo(createResponse.PaymentId));
    }
}