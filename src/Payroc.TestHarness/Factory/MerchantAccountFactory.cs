// TODO: Re-instate once Boarding and Funding are available in SDK again
/*
using Payroc.Boarding.MerchantPlatforms;

namespace Payroc.TestHarness.Factory;

public class MerchantAccountFactory
{
    public static CreateMerchantAccount Create(string pricingIntentId = "1602")
        => new ()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            Business = BusinessFactory.Create(),
            ProcessingAccounts = [ CreateProcessingAccountFactory.Create(pricingIntentId) ]
        };
}
*/
