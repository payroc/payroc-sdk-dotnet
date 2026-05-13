using Payroc;
using Payroc.BankTransferPayments.Payments;
using Payroc.TestCommon.HelperMethods.BankTransfer;

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
        createRequest.Order.Currency = Currency.Cad;
        createRequest.Order.Breakdown = null;
        createRequest.CredentialOnFile = null;
        createRequest.CustomFields = null;
        createRequest.PaymentMethod = PadPaymentMethodFactory.Create();
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
