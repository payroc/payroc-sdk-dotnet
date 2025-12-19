using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;

namespace Payroc.TestFunctional.Boarding.MerchantPlatforms;

[TestFixture, Category("Boarding.MerchantPlatforms")]
[NonParallelizable]
public class CreateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var pricingIntentBody = Factories.Boarding.RequestBodies.PricingIntentFactory.Create();
        var pricingIntentRequest = Data.Get<CreatePricingIntentsRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.Body, pricingIntentBody )
            ]);
        var pricingIntentResponse = await client.Boarding.PricingIntents.CreateAsync(pricingIntentRequest);
       
        var merchantAccountRequest = Data.Get<CreateMerchantAccount>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
            ]);
        merchantAccountRequest.Business.TaxId = HelperMethods.Boarding.TaxCodeGenerator.Generate();
        merchantAccountRequest.ProcessingAccounts.First().Pricing = new Pricing.Intent(new PricingTemplate
        {
            PricingIntentId = pricingIntentResponse.Id ?? throw new Exception("Pricing Intent ID is null")
        });

        var merchantAccountResponse = await client.Boarding.MerchantPlatforms.CreateAsync(merchantAccountRequest);
            
        Assert.That(merchantAccountResponse.MerchantPlatformId, Is.Not.Null);
    }
}
