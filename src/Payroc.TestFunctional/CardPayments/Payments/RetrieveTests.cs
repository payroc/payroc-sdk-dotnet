using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.CardPayments.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTestAvs()
    {
        var client = GlobalFixture.Payments;
       
        var paymentAvsRequest = Data.Get<PaymentRequest>([
            ( i => i.ProcessingTerminalId,  GlobalFixture.TerminalIdAvs ),
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
       
        var paymentResponseAvs = await client.CardPayments.Payments.CreateAsync(paymentAvsRequest);
        var retrieveAvsRequest = new RetrievePaymentsRequest()
        {
            PaymentId = paymentResponseAvs.PaymentId
        };

        var retrievedPamentAvsResponse = await client.CardPayments.Payments.RetrieveAsync(retrieveAvsRequest);

        Assert.That(retrievedPamentAvsResponse.TransactionResult, Is.Not.Null);
    }
    
    [Test]
    public async Task SmokeTestNoAvs()
    {
        var client = GlobalFixture.Payments;
        var paymentNoAvsRequest = Data.Get<PaymentRequest>([
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdNoAvs ),
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
       
        var paymentNoAvsResponse = await client.CardPayments.Payments.CreateAsync(paymentNoAvsRequest);
        var retrieveNoAvsRequest = new RetrievePaymentsRequest()
        {
            PaymentId =  paymentNoAvsResponse.PaymentId
        };
        
        var retrievedPamentNoAvsResponse = await client.CardPayments.Payments.RetrieveAsync(retrieveNoAvsRequest);

        Assert.That(retrievedPamentNoAvsResponse.TransactionResult, Is.Not.Null);
    }
}
