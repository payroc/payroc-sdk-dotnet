using Payroc.Reporting.Settlement;

namespace Payroc.TestCommon.Tests.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListDisputeStatusesTests
{
    [Test]
    public async Task Settlement_ListDisputeStatuses_Success()
    {
        var client = GlobalFixture.Payments;
        
        // Using a dispute ID that is unlikely to exist to demonstrate the test structure without relying on specific data
        // Todo : Replace with a valid DisputeId for a more meaningful test
        var listDisputeStatusesRequest = new ListDisputesStatusesSettlementRequest
        {
            DisputeId = 65
        };

        var disputeStatusesResponse = await client.Reporting.Settlement.ListDisputesStatusesAsync(listDisputeStatusesRequest);

        Assert.That(disputeStatusesResponse, Is.Not.Null);
    } 
}
