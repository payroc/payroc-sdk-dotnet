using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestFunctional.Factories.Boarding.RequestBodies;

namespace Payroc.TestFunctional.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[Parallelizable(ParallelScope.Fixtures)]

public class ListProcessingAccountFundingAccountsTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var listFundingAccountsRequest = new ListProcessingAccountFundingAccountsRequest
        {
            ProcessingAccountId = BoardingTestFixture.SharedProcessingAccountId
        };
        
        var fundingAccountsResponse = await client.Boarding.ProcessingAccounts.ListProcessingAccountFundingAccountsAsync(listFundingAccountsRequest);
        
        Assert.That(fundingAccountsResponse.First(), Is.Not.Null);
        Assert.That(fundingAccountsResponse.First().FundingAccountId.HasValue, Is.True);
    }
}
