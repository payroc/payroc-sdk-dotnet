using Payroc.Funding.FundingAccounts;
using Payroc.Funding.FundingRecipients;

namespace Payroc.TestFunctional.Funding.FundingAccounts;

[TestFixture, Category("Funding.FundingAccounts")]
[Parallelizable(ParallelScope.Fixtures)]
public class DeleteTests
{
    [Test]
    [Ignore("Data issues: Failing KYC checks - needs investigation")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<CreateFundingRecipient>([
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() )
        ]);
        var response = await client.Funding.FundingRecipients.CreateAsync(request);
        var deleteAccountsRequest = new DeleteFundingAccountsRequest()
        {
            FundingAccountId = response?.FundingAccounts?.First().FundingAccountId ?? throw new Exception("Funding Account ID is null")
        };
        
        // No response body returned for delete, so we just ensure no exception is thrown.
        Assert.DoesNotThrowAsync(async () => await client.Funding.FundingAccounts.DeleteAsync(deleteAccountsRequest));
    }
}
