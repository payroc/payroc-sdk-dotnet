using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestCommon.Factories.Boarding.RequestBodies;
using Payroc.TestCommon.HelperMethods.Boarding;

namespace Payroc.TestCommon.Tests.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[NonParallelizable]
public class ListOwnersTests
{
    [Test]
    public async Task ProcessingAccounts_ListOwners_Success()
    {
        var client = GlobalFixture.Payments;
        var pricingIntentBody = Payroc.TestCommon.Factories.Boarding.RequestBodies.PricingIntentFactory.Create();
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
        merchantAccountRequest.Business.TaxId = Payroc.TestCommon.HelperMethods.Boarding.TaxCodeGenerator.Generate();
        merchantAccountRequest.ProcessingAccounts.First().Pricing = new Pricing.Intent(new PricingTemplate
        {
            PricingIntentId = pricingIntentResponse.Id ?? throw new Exception("Pricing Intent ID is null")
        });
        MerchantAccountHelper.ApplyStableCardAcceptance(merchantAccountRequest);
        var merchantAccountResponse = await client.Boarding.MerchantPlatforms.CreateAsync(merchantAccountRequest);
        var processingAccountRequest = new CreateProcessingAccountMerchantPlatformsRequest
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            MerchantPlatformId = merchantAccountResponse.MerchantPlatformId ?? string.Empty,
            Body = ProcessingAccountRequestFactory.Create(pricingIntentResponse.Id)
        };
        var processingAccountResponse = await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(processingAccountRequest);
        _ = await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(processingAccountRequest);
        _ = await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(processingAccountRequest);
        var listOwnersRequest = new ListProcessingAccountOwnersRequest()
        {
            ProcessingAccountId = processingAccountResponse.ProcessingAccountId ?? string.Empty,
            Limit = 3
        };
        
        var listOwnersResponse = await client.Boarding.ProcessingAccounts.ListOwnersAsync(listOwnersRequest);
        
        Assert.That(processingAccountResponse.ProcessingAccountId, Is.Not.Null);
        Assert.That(listOwnersResponse.CurrentPage.Items.Count, Is.GreaterThan(1));
    }
}
