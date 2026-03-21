using Payroc.BankTransferPayments.Payments;
using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestCommon.Tests.Payments.BankTransferPayments;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class ReversePaymentTests
{
    [Test]
    public async Task BankTransferRefunds_ReversePayment_Success()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad )
        ]);
        createRequest.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
        var createResponse = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        var reverseRequest = Data.Get<ReversePaymentRefundsRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.PaymentId, createResponse.PaymentId ),
            ]);
        
        try
        {
            var reverseResponse = await client.BankTransferPayments.Refunds.ReversePaymentAsync(reverseRequest);
            Assert.That(reverseResponse.PaymentId, Is.Not.Null);
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
