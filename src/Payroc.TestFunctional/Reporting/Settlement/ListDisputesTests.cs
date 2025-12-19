using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListDisputesTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Generic;
        
        // Using a date that is unlikely to have disputes to demonstrate the test structure without relying on specific data
        // Todo : Replace with a date known to have disputes for a more meaningful test
        var listDisputesRequest = new ListReportingSettlementDisputesRequest
        {
            Date = new DateOnly(2025, 09, 14),
            Limit = 1
        };
        
        var disputeResponse = await client.Reporting.Settlement.ListDisputesAsync(listDisputesRequest);
        
        Assert.That(disputeResponse.CurrentPage.Items.Count, Is.EqualTo(0));
    } 
}
