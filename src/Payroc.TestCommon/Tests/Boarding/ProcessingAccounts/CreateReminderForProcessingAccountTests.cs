using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestCommon.Factories.Boarding.RequestBodies;

namespace Payroc.TestCommon.Tests.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[NonParallelizable]
public class CreateReminderForProcessingAccountTests
{
    [Test]
    public async Task ProcessingAccounts_CreateReminderForProcessingAccount_Success()
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
        var reminderRequest = new CreateReminderProcessingAccountsRequest()
        {
            IdempotencyKey =  Guid.NewGuid().ToString(),
            ProcessingAccountId = processingAccountResponse.ProcessingAccountId ?? string.Empty,
            Body = new CreateReminderProcessingAccountsRequestBody.PricingAgreement()
        };
        
        var reminderResponse = await client.Boarding.ProcessingAccounts.CreateReminderAsync(reminderRequest);
        
        Assert.That(processingAccountResponse.ProcessingAccountId, Is.Not.Null);
        Assert.That(reminderResponse.IsPricingAgreement, Is.True);
        Assert.That(reminderResponse.AsPricingAgreement().ReminderId, Is.Not.Null);
    }
}
