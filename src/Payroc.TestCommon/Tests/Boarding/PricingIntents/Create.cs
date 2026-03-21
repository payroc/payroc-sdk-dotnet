using Payroc.Boarding.PricingIntents;

namespace Payroc.TestCommon.Tests.Boarding.PricingIntents;

[TestFixture, Category("Boarding.PricingIntents")]
[NonParallelizable]
public class Create
{
    [Test]
    public async Task PricingIntents_Create_Success()
    {
        var client = GlobalFixture.Payments;
        var pricingIntentRequest = Data.Get<CreatePricingIntentsRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.Body, Payroc.TestCommon.Factories.Boarding.RequestBodies.PricingIntentFactory.Create())
        ]);
        
        var pricingIntentResponse = await client.Boarding.PricingIntents.CreateAsync(pricingIntentRequest);
        
        Assert.That(pricingIntentResponse.Id, Is.Not.Null);
    }
}
