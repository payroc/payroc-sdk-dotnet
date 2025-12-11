using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.TestFunctional.Factories.Boarding.RequestBodies;

namespace Payroc.TestFunctional.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListProcessingTerminalsTests
{
     [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var listProcessingTerminalsRequest = new ListProcessingTerminalsProcessingAccountsRequest()
        {
            ProcessingAccountId = BoardingTestFixture.SharedProcessingAccountId
        };
        var listProcessingTerminalsResponse = await client.Boarding.ProcessingAccounts.ListProcessingTerminalsAsync(listProcessingTerminalsRequest);

        Assert.That(listProcessingTerminalsResponse.CurrentPage, Is.Not.Null);
    }
}
