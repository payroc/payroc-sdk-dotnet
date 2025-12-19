using Payroc.Boarding.PricingIntents;

namespace Payroc.TestFunctional.Boarding.PricingIntents;

[TestFixture, Category("Boarding.PricingIntents")]
[NonParallelizable]
public class PartiallyUpdateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var pricingIntentRequest = Data.Get<CreatePricingIntentsRequest>(
        [
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, Factories.Boarding.RequestBodies.PricingIntentFactory.Create())
        ]);
        var pricingIntentResponse = await client.Boarding.PricingIntents.CreateAsync(pricingIntentRequest);
        var partiallyUpdateRequest = new PartiallyUpdatePricingIntentsRequest()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            PricingIntentId = pricingIntentResponse.Id ?? throw new Exception("Pricing Intent ID is null"),
            Body = [
                new(new PatchDocument.Add(new()
                {
                    Path = "/processor/card/fees/mastercardVisaDiscover/qualifiedRate/volume",
                    Value = 1.5
                })),
                new(new PatchDocument.Replace(new()
                {
                    Path = "processor/card/fees/pinDebit/additionalDiscount",
                    Value = 0.35
                })),
                new(new PatchDocument.Replace(new()
                {
                    Path = "processor/card/fees/pinDebit/transaction",
                    Value = 4
                }))
            ]
        };
        
        var partiallyUpdatedResponse = await client.Boarding.PricingIntents.PartiallyUpdateAsync(partiallyUpdateRequest); 
        
        Assert.That(partiallyUpdatedResponse.Id, Is.Not.Null);
    }
}
