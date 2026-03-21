using Payroc.BankTransferPayments.Payments;

namespace Payroc.TestCommon.Tests.BankTransferPayments.Payments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class RepresentTests
{
    [Test]
    [Ignore("Data Issues: BankTransferPayments use PAD transactions. Representment does not currently work for PAD transactions. Will re-enable once this is resolved.")]
    public async Task BankTransferPayments_Represent_Success()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad )
        ]);
        createRequest.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
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
