using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTransactionsTests
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
        var listTransactionsRequest = new ListReportingSettlementTransactionsRequest
        {
            BatchId = batchResponse.CurrentPage.Items[0].BatchId ?? 0,
            Date = new DateOnly(2025, 09, 14),
            Limit = 1
        };
        
        var transactionResponse = await client.Reporting.Settlement.ListTransactionsAsync(listTransactionsRequest);

        Assert.That(transactionResponse.CurrentPage.Items.Count, Is.EqualTo(1));
    }
}
