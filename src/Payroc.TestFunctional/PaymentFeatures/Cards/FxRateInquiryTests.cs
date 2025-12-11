using Payroc.PaymentFeatures.Cards;

namespace Payroc.TestFunctional.Payments.CurrencyConversion;

[TestFixture, Category("Payments.CurrencyConversion")]
[Parallelizable(ParallelScope.Fixtures)]
public class FxRateInquiryTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<FxRateInquiry>([
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        
        var response = await client.PaymentFeatures.Cards.RetrieveFxRatesAsync(request);
        
        Assert.That(response.InquiryResult.DccOffered, Is.False);
        Assert.That(response.BaseCurrency, Is.EqualTo(request.BaseCurrency));
        Assert.That(request.BaseAmount > 0, Is.True);
    }
}
