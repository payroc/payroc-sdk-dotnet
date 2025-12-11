using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestFunctional.Factories.Boarding.RequestBodies;

namespace Payroc.TestFunctional.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTerminalOrdersTests
{
     [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var listTerminalOrdersRequest = new ListTerminalOrdersProcessingAccountsRequest()
        {
            ProcessingAccountId = BoardingTestFixture.SharedProcessingAccountId
        };
        
        var listTerminalOrdersResponse = await client.Boarding.ProcessingAccounts.ListTerminalOrdersAsync(listTerminalOrdersRequest);

        Assert.That(listTerminalOrdersResponse.First().OrderItems, Is.Not.Empty); 
    }
}
