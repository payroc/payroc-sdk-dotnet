using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestFunctional.Factories.Boarding.RequestBodies;

namespace Payroc.TestFunctional.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[NonParallelizable]
public class CreateTerminalOrderTests
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
        var createTerminalOrderRequest = Data.Get<CreateTerminalOrder>([
            ( i => i.IdempotencyKey,  Guid.NewGuid().ToString() ),
            ( i => i.ProcessingAccountId, processingAccountResponse.ProcessingAccountId ?? string.Empty )
        ]);

        var terminalOrderResponse =  await client.Boarding.ProcessingAccounts.CreateTerminalOrderAsync(createTerminalOrderRequest);

        Assert.That(processingAccountResponse.ProcessingAccountId, Is.Not.Null);
        Assert.That(terminalOrderResponse.TerminalOrderId, Is.Not.Null);
    }
}
