using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;

namespace Payroc.TestFunctional.Boarding.MerchantPlatforms;

[TestFixture, Category("Boarding.MerchantPlatforms")]
[NonParallelizable]
public class ListTests
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
        var merchantAccountRequest2 = Data.Get<CreateMerchantAccount>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        merchantAccountRequest2.Business.TaxId = HelperMethods.Boarding.TaxCodeGenerator.Generate();
        merchantAccountRequest2.ProcessingAccounts.First().Pricing = new Pricing.Intent(new PricingTemplate
        {
            PricingIntentId = pricingIntentResponse.Id ?? throw new Exception("Pricing Intent ID is null")
        });
        _ = await client.Boarding.MerchantPlatforms.CreateAsync(merchantAccountRequest2);
        var listRequest = new ListMerchantPlatformsRequest
        {
            Limit = 2
        };
        
        var listResponse = await client.Boarding.MerchantPlatforms.ListAsync(listRequest);
        
        Assert.That(merchantAccountResponse.MerchantPlatformId, Is.Not.Null);
        Assert.That(listResponse.CurrentPage.Items.Count, Is.GreaterThan(1));
    }
}
