using Payroc.Funding.FundingAccounts;
using Payroc.Funding.FundingRecipients;

namespace Payroc.TestFunctional.Funding.FundingAccounts;

[TestFixture, Category("Funding.FundingRecipients")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
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
        var retrieveRequest = new RetrieveFundingAccountsRequest
        {
            FundingAccountId = response.FundingAccounts.First().FundingAccountId.Value 
        };
        
        var retrieveResponse =  await client.Funding.FundingAccounts.RetrieveAsync(retrieveRequest);
        
        Assert.That(response.RecipientId, Is.Not.Null);
        Assert.That(retrieveResponse.FundingAccountId, Is.Not.Null);
    }
}
