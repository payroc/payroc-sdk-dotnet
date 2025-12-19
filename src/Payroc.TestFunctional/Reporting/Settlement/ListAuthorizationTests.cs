using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListAuthorizationTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Generic;
        var retrieveBatchesRequest = new ListReportingSettlementBatchesRequest
        {
            Date = new DateOnly(2025, 09, 14)
        };
        var batchResponse = await client.Reporting.Settlement.ListBatchesAsync(retrieveBatchesRequest);
        _ = await client.Reporting.Settlement.ListBatchesAsync(retrieveBatchesRequest);
        _ = await client.Reporting.Settlement.ListBatchesAsync(retrieveBatchesRequest);
        var authorizationsRequest = new ListReportingSettlementAuthorizationsRequest
        {
            BatchId = batchResponse.CurrentPage.Items[0].BatchId ?? 0,
            Date = new DateOnly(2025, 09, 14),
        };
        
        var authorizationResponse = await client.Reporting.Settlement.ListAuthorizationsAsync(authorizationsRequest);

        Assert.That(authorizationResponse.CurrentPage.Items.Count, Is.GreaterThanOrEqualTo(1));
    }
}
