using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.TestCommon.Factories.Boarding.RequestBodies;

namespace Payroc.TestCommon.Tests.Boarding.MerchantPlatforms;

[TestFixture, Category("Boarding.MerchantPlatforms")]
[NonParallelizable]
public class ListProcessingAccountTests
{
    [Test]
    public async Task MerchantPlatforms_ListProcessingAccount_Success()
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
        var listProcessingAccountRequest = new ListBoardingMerchantPlatformProcessingAccountsRequest
        {
            MerchantPlatformId = merchantAccountResponse.MerchantPlatformId ?? throw new Exception("Merchant Platform ID is null"),
            Limit = 2
        };
        var listProcessingAccountResponse = await client.Boarding.MerchantPlatforms.ListProcessingAccountsAsync(listProcessingAccountRequest);
        
        Assert.That(processingAccountResponse.ProcessingAccountId, Is.Not.Null);
        Assert.That(listProcessingAccountResponse.CurrentPage.Items.Count, Is.GreaterThan(1));
    }
}