using Payroc.Boarding.PricingIntents;

namespace Payroc.TestFunctional.Boarding.PricingIntents;

[TestFixture, Category("Boarding.PricingIntents")]
[NonParallelizable]
public class DeleteTests
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
        
        var deletePricingIntentRequest = new DeletePricingIntentsRequest
        {
            PricingIntentId = pricingIntentResponse.Id ?? throw new Exception("Pricing Intent ID is null")
        };

        // There is no response body for delete, so we just ensure no exception was thrown
        Assert.DoesNotThrowAsync(async () =>
        {
            await client.Boarding.PricingIntents.DeleteAsync(deletePricingIntentRequest);
        });
    }
}
