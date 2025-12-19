using Payroc.Boarding.ProcessingAccounts;

namespace Payroc.TestFunctional.Boarding.ProcessingAccounts;

[TestFixture, Category("Boarding.ProcessingAccounts")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListContactsTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var listContactsRequest = new ListContactsProcessingAccountsRequest
        {
            ProcessingAccountId = BoardingTestFixture.SharedProcessingAccountId,
            Limit = 3
        };
        
        var listContactsResponse = await client.Boarding.ProcessingAccounts.ListContactsAsync(listContactsRequest);
        
        Assert.That(listContactsResponse.HasMore, Is.Not.Null);
        Assert.That(listContactsResponse.Count, Is.GreaterThan(0));
    }
}
