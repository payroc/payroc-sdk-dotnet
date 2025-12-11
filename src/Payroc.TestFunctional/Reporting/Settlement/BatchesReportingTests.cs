using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class BatchesReportingTests
{
    private static string NewKey() => Guid.NewGuid().ToString();

    [Test]
    public async Task Batches_Reporting_Lifecycle()
    {
        var client = GlobalFixture.Generic;

        // get the initial list of batches (should be 1)
        var response = await client.Reporting.Settlement.ListBatchesAsync(
            new ListReportingSettlementBatchesRequest
            {
                Date = new DateOnly(2025, 09, 14)
            }
        );

        Assert.That(response.CurrentPage.Items.Count, Is.EqualTo(1));

        // now retrieve the full batch
        var batchResponse = await client.Reporting.Settlement.RetrieveBatchAsync(
            new RetrieveBatchSettlementRequest { BatchId = response.CurrentPage.Items[0].BatchId ?? 0 }
        );

        Assert.Multiple(() =>
        {
            Assert.That(batchResponse.SaleAmount, Is.EqualTo(270375));
            Assert.That(batchResponse.TransactionCount, Is.EqualTo(50));
            Assert.That(batchResponse?.Merchant?.MerchantId, Is.EqualTo("403903000102545"));
        });
    }
}