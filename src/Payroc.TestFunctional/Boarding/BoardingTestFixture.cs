using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestFunctional.Factories.Boarding.RequestBodies;

namespace Payroc.TestFunctional.Boarding;

[SetUpFixture]
public class BoardingTestFixture
{
    public static string SharedPricingIntentId { get; private set; } = null!;
    public static string SharedMerchantPlatformId { get; private set; } = null!;
    public static string SharedProcessingAccountId { get; private set; } = null!;
    public static string SharedContactId { get; private set; } = null!;
    public static string SharedOwnerId { get; private set; } = null!;
    public static string SharedTerminalOrderId { get; private set; } = null!;
    public static string SharedProcessingTerminalId { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        var client = GlobalFixture.Payments;
        
        var pricingIntentBody = PricingIntentFactory.Create();
        var pricingIntentRequest = Data.Get<CreatePricingIntentsRequest>(
        [
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.Body, pricingIntentBody)
        ]);
        var pricingIntent = await client.Boarding.PricingIntents.CreateAsync(pricingIntentRequest);
        SharedPricingIntentId = pricingIntent.Id ?? throw new Exception("Failed to create shared PricingIntent");
        
        var merchantRequest = Data.Get<CreateMerchantAccount>(
        [
            (i => i.IdempotencyKey, Guid.NewGuid().ToString())
        ]);
        merchantRequest.Business.TaxId = HelperMethods.Boarding.TaxCodeGenerator.Generate();
        merchantRequest.ProcessingAccounts.First().Pricing = new Pricing.Intent(new PricingTemplate
        {
            PricingIntentId = SharedPricingIntentId
        });
        var merchant = await client.Boarding.MerchantPlatforms.CreateAsync(merchantRequest);
        SharedMerchantPlatformId = merchant.MerchantPlatformId ?? throw new Exception("Failed to create shared MerchantPlatform");
        
        var processingRequest = new CreateProcessingAccountMerchantPlatformsRequest
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            MerchantPlatformId = SharedMerchantPlatformId,
            Body = ProcessingAccountRequestFactory.Create(SharedPricingIntentId)
        };
        var processing = await client.Boarding.MerchantPlatforms.CreateProcessingAccountAsync(processingRequest);
        SharedProcessingAccountId = processing.ProcessingAccountId ?? throw new Exception("Failed to create shared ProcessingAccount");
        SharedContactId = processing.Contacts?.FirstOrDefault()?.ContactId?.ToString() ?? throw new Exception("No contacts found in ProcessingAccount");
        SharedOwnerId = processing.Owners?.FirstOrDefault()?.OwnerId?.ToString() ?? throw new Exception("No owners found in ProcessingAccount");
        SharedProcessingTerminalId = "5984001";//GlobalFixture.TerminalIdAvs;// "placeholder-terminal-id"; // TODO: Populate this to fix RetrieveHostProcessorConfiguration
        
        var terminalOrderRequest = Data.Get<CreateTerminalOrder>(
        [
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
            (i => i.ProcessingAccountId, SharedProcessingAccountId)
        ]);
        var terminalOrder = await client.Boarding.ProcessingAccounts.CreateTerminalOrderAsync(terminalOrderRequest);
        SharedTerminalOrderId = terminalOrder.TerminalOrderId ?? throw new Exception("Failed to create shared TerminalOrder");
    }
}
