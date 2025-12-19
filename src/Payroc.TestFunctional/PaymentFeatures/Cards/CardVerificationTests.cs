using Payroc.PaymentFeatures.Cards;

namespace Payroc.TestFunctional.PaymentFeatures.Cards;

[TestFixture, Category("PaymentFeatures.Cards")]
[Parallelizable(ParallelScope.Fixtures)]
public class CardVerificationTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<CardVerificationRequest>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString())
        ]);

        var response = await client.PaymentFeatures.Cards.VerifyCardAsync(request);
        
        Assert.That(response?.TransactionResult?.Status, Is.Not.Null);
    }
}
