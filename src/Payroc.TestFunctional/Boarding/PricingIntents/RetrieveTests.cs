using Payroc.Boarding.PricingIntents;

namespace Payroc.TestFunctional.Boarding.PricingIntents;

[TestFixture, Category("Boarding.PricingIntents")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var retrieveRequest = new RetrievePricingIntentsRequest
        {
            PricingIntentId = BoardingTestFixture.SharedPricingIntentId
        };
        
        var retrieveResponse = await client.Boarding.PricingIntents.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.Id, Is.EqualTo(BoardingTestFixture.SharedPricingIntentId));
    }
}
