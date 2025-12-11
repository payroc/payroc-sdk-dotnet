using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestFunctional.Factories.Boarding.RequestBodies;

namespace Payroc.TestFunctional.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveProcessingAccountTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var retrieveRequest = new RetrieveProcessingAccountsRequest
        {
            ProcessingAccountId = BoardingTestFixture.SharedProcessingAccountId
        };
        
        var retrieveResponse = await client.Boarding.ProcessingAccounts.RetrieveAsync(retrieveRequest);
        
        Assert.That(retrieveResponse.ProcessingAccountId, Is.EqualTo(BoardingTestFixture.SharedProcessingAccountId));
    }
}
