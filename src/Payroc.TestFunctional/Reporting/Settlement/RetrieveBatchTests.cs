using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveBatchTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Generic;
        var listBatchesRequest = new ListReportingSettlementBatchesRequest
        {
            Date = new DateOnly(2025, 09, 14)
        };
        var listBatchesResponse = await client.Reporting.Settlement.ListBatchesAsync(listBatchesRequest);
        var retrieveBatchSettlementRequest = new RetrieveBatchSettlementRequest
        {
            BatchId = listBatchesResponse.CurrentPage.Items.First().BatchId ?? 0
        };

        var retrieveBatchSettlementResponse = await client.Reporting.Settlement.RetrieveBatchAsync(retrieveBatchSettlementRequest);

        Assert.That(retrieveBatchSettlementResponse.BatchId, Is.Not.Null);
    }
}
