using Payroc.PaymentFeatures.Cards;

namespace Payroc.TestCommon.Tests.PaymentFeatures.Cards;

[TestFixture, Category("PaymentFeatures.Cards")]
[Parallelizable(ParallelScope.Fixtures)]
public class FxRateInquiryTests
{
    [Test]
    public async Task Cards_FxRateInquiry_Success()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<FxRateInquiry>([
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        
        var response = await client.PaymentFeatures.Cards.RetrieveFxRatesAsync(request);

        Assert.That(response.BaseCurrency, Is.EqualTo(request.BaseCurrency));
        Assert.That(request.BaseAmount > 0, Is.True);
    }
}
