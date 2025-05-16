using Payroc.Boarding.MerchantPlatforms;

namespace Payroc.TestHarness.Factory;

public class MerchantAccountFactory
{
    public static CreateMerchantAccount Create()
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            Business = BusinessFactory.Create(),
            ProcessingAccounts = [ CreateProcessingAccountFactory.Create() ]
        };
}
