using Payroc.Boarding.PricingIntents;

namespace Payroc.TestFunctional.Boarding.PricingIntents;

[TestFixture, Category("Boarding.PricingIntents")]
[NonParallelizable]
public class ListTests
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
        _ = await client.Boarding.PricingIntents.CreateAsync(pricingIntentRequest);
        _ = await client.Boarding.PricingIntents.CreateAsync(pricingIntentRequest);
        var listPricingIntentsRequest = new ListPricingIntentsRequest
        {
            Limit = 10
        };
        
        var listedPricingIntentsResponse =  await client.Boarding.PricingIntents.ListAsync(listPricingIntentsRequest);
        
        Assert.That(pricingIntentResponse, Is.Not.Null);
        Assert.That(listedPricingIntentsResponse.CurrentPage.Items.Count, Is.GreaterThan(1));
    }
}
