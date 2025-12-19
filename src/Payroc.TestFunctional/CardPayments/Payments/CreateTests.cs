using Payroc.CardPayments.Payments;

namespace Payroc.TestFunctional.CardPayments.Payments;

[TestFixture, Category("CardPayments.Payments")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task SmokeTestAvs()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<PaymentRequest>([
            ( i => i.ProcessingTerminalId,  GlobalFixture.TerminalIdAvs ),
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);

        var response = await client.CardPayments.Payments.CreateAsync(request);

        Assert.That(response.TransactionResult.Status, Is.EqualTo("ready"));
        Assert.That(response.TransactionResult.ResponseMessage, Is.EqualTo("APPROVAL"));
        Assert.That(response.TransactionResult.ResponseCode, Is.EqualTo("A"));
        Assert.That(response.TransactionResult.ProcessorResponseCode, Is.EqualTo("00"));
    }

    [Test]
    public async Task SmokeTestNoAvs()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<PaymentRequest>([
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdNoAvs ),
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        
        var response = await client.CardPayments.Payments.CreateAsync(request);
        
        Assert.That(response.TransactionResult, Is.Not.Null);
    }
}
