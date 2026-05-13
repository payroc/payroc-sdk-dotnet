using Payroc;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestCommon.Factories.Boarding.RequestBodies;
using Payroc.TestCommon.HelperMethods.Boarding;

namespace Payroc.TestCommon.Tests.Boarding;

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
        try
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
            merchantRequest.Business.TaxId = Payroc.TestCommon.HelperMethods.Boarding.TaxCodeGenerator.Generate();
            // UAT enforces uniqueness on owner/contact nationalId. The hardcoded SSN in CreateMerchantAccount.json
            // persists between runs, causing ConflictError (409) on the second run. Generate a unique value each run.
            var uniqueSsn = $"{Random.Shared.Next(100, 999):D3}-{Random.Shared.Next(10, 99):D2}-{Random.Shared.Next(1000, 9999):D4}";
            var ownerIdentifier = merchantRequest.ProcessingAccounts.First().Owners?.FirstOrDefault()?.Identifiers?.FirstOrDefault(i => i.Type == IdentifierType.NationalId);
            if (ownerIdentifier != null) ownerIdentifier.Value = uniqueSsn;
            var contactIdentifier = merchantRequest.ProcessingAccounts.First().Contacts?.FirstOrDefault()?.Identifiers?.FirstOrDefault(i => i.Type == IdentifierType.NationalId);
            if (contactIdentifier != null) contactIdentifier.Value = uniqueSsn;
            merchantRequest.ProcessingAccounts.First().Pricing = new Pricing.Intent(new PricingTemplate
            {
                PricingIntentId = SharedPricingIntentId
            });
            MerchantAccountHelper.ApplyStableCardAcceptance(merchantRequest);
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
            // Pin to a solutionTemplateId known to be accepted by the UAT API.
            var terminalOrderItem = terminalOrderRequest.OrderItems.FirstOrDefault();
            if (terminalOrderItem != null)
                terminalOrderItem.SolutionTemplateId = "VAR_Only_TSYS";
            var terminalOrder = await client.Boarding.ProcessingAccounts.CreateTerminalOrderAsync(terminalOrderRequest);
            SharedTerminalOrderId = terminalOrder.TerminalOrderId ?? throw new Exception("Failed to create shared TerminalOrder");
        }
        catch (PayrocApiException ex)
        {
            Assert.Fail($"Exception thrown during BoardingTestFixture setup: {ex}");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception thrown during BoardingTestFixture setup: {ex}");
        }
    }
}
