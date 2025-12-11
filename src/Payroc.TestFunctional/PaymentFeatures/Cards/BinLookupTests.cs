using Payroc.PaymentFeatures.Cards;

namespace Payroc.TestFunctional.Payments.Cards;

[TestFixture, Category("Payments.Cards")]
[Parallelizable(ParallelScope.Fixtures)]
public class BinLookupTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<BinLookup>([
            (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs)
        ]);
        
        var response = await client.PaymentFeatures.Cards.LookupBinAsync(request);
        
        Assert.That(response.Type, Is.Not.Null);       
    }
}
