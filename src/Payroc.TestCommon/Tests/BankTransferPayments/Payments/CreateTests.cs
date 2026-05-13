using Payroc;
using Payroc.BankTransferPayments.Payments;
using Payroc.TestCommon.HelperMethods.BankTransfer;

namespace Payroc.TestCommon.Tests.BankTransferPayments.Payments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task BankTransferPayments_Create_Success()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad ),
        ]);
        request.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
        request.Order.Currency = Currency.Cad;
        request.Order.Breakdown = null;
        request.CredentialOnFile = null;
        request.CustomFields = null;
        request.PaymentMethod = PadPaymentMethodFactory.Create();
        
        try
        {
            var response = await client.BankTransferPayments.Payments.CreateAsync(request);
            Assert.That(response.PaymentId, Is.Not.Null);
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
