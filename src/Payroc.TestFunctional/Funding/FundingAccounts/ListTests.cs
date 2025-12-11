using System.Runtime.InteropServices.ComTypes;
using Payroc.Funding.FundingAccounts;

namespace Payroc.TestFunctional.Funding.FundingAccounts;

using Payroc.Funding.FundingRecipients;

[TestFixture, Category("Funding.FundingRecipients")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
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

        var listAccountsRequest = new ListFundingAccountsRequest()
        {
            Limit = 10
        };
        
        var listAccountsResponse =  await client.Funding.FundingAccounts.ListAsync(listAccountsRequest);
        
        Assert.That(response.RecipientId, Is.Not.Null);
        Assert.That(listAccountsResponse.CurrentPage.Items.Count, Is.GreaterThan(0));
    }
}
