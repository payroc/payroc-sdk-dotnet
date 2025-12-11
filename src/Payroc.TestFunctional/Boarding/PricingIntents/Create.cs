using Payroc.Boarding.PricingIntents;

namespace Payroc.TestFunctional.Boarding.PricingIntents;

[TestFixture, Category("Boarding.PricingIntents")]
[NonParallelizable]
public class Create
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var pricingIntentRequest = Data.Get<CreatePricingIntentsRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.Body, Factories.Boarding.RequestBodies.PricingIntentFactory.Create())
        ]);
        
        var pricingIntentResponse = await client.Boarding.PricingIntents.CreateAsync(pricingIntentRequest);
        
        Assert.That(pricingIntentResponse.Id, Is.Not.Null);
    }
}
