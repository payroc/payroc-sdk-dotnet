using Payroc.Funding.FundingAccounts;
using Payroc.Funding.FundingRecipients;
using Payroc.TestFunctional.Factories.Funding.RequestBodies;

namespace Payroc.TestFunctional.Funding.FundingAccounts;

[TestFixture, Category("Funding.FundingAccounts")]
[Parallelizable(ParallelScope.Fixtures)]
public class UpadteTests
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
        var updateAccountsRequest = new UpdateFundingAccountsRequest()
        {
            FundingAccountId = response.FundingAccounts.First().FundingAccountId ?? throw new Exception("Funding Account ID is null"),
            Body = FundingAccountFactory.Create()
        };
        
        // No response body returned for update, so we just ensure no exception is thrown.
        Assert.DoesNotThrowAsync(async () => await client.Funding.FundingAccounts.UpdateAsync(updateAccountsRequest));
    }
}
