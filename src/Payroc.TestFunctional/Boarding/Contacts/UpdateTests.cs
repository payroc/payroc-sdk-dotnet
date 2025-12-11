using Payroc.Boarding.Contacts;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.Owners;
using Payroc.Boarding.PricingIntents;
using Payroc.TestFunctional.Factories.Boarding.RequestBodies;

namespace Payroc.TestFunctional.Boarding.Contacts;

[TestFixture, Category("Boarding.Contacts")]
[NonParallelizable]
public class UpdateTests
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
        var processingAccountRequest = new CreateProcessingAccountMerchantPlatformsRequest
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            MerchantPlatformId = merchantAccountResponse.MerchantPlatformId ?? string.Empty,
            Body = ProcessingAccountRequestFactory.Create(pricingIntentResponse.Id)
        };
        var processingAccountResponse = await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(processingAccountRequest);
        var body = Factories.Boarding.RequestBodies.ProcessingAccountRequestFactory.Create();
        body.Contacts.First().MiddleName = new string("Mandy");
        var contactsRequest = new UpdateContactsRequest()
        {
            ContactId = processingAccountResponse.Contacts.First().ContactId.Value,
            Body = body.Contacts.First()
        };

        // There is no response body for update, so we just ensure no exception was thrown     
        Assert.DoesNotThrowAsync(async () =>
        {
            await client.Boarding.Contacts.UpdateAsync(contactsRequest);
        });
    }
}