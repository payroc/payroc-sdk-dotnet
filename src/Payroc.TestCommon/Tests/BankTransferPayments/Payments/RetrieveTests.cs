

using Payroc.BankTransferPayments.Payments;

namespace Payroc.TestCommon.Tests.BankTransferPayments.Payments;

[TestFixture, Category("BankTransferPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.PaymentsBank;
        var createRequest = Data.Get<BankTransferPaymentRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBankPad )
        ]);
        createRequest.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
        var createResponse = await client.BankTransferPayments.Payments.CreateAsync(createRequest);
        var retrieveRequest = new RetrievePaymentsRequest
        {
            PaymentId = createResponse.PaymentId,
        };
        
        var retrieveResponse = await client.BankTransferPayments.Payments.RetrieveAsync(retrieveRequest);

        Assert.That(retrieveResponse.PaymentId, Is.EqualTo(createResponse.PaymentId));
    }
}