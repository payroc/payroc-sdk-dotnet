using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class TransactionsReportingTests
{
    [Test]
    public async Task Transactions_Reporting_Lifecycle()
    {
        var client = GlobalFixture.Generic;

        // get the initial list of batches (should be 1)
        var batchResponse = await client.Reporting.Settlement.ListBatchesAsync(
            new ListReportingSettlementBatchesRequest
            {
                Date = new(2025, 09, 14)
            }
        );

        // get the initial list of transactions (should be 1)
        var transResponse = await client.Reporting.Settlement.ListTransactionsAsync(
            new ListReportingSettlementTransactionsRequest
            {
                BatchId = batchResponse.CurrentPage.Items[0].BatchId ?? 0,
                Date = new(2025, 09, 14),
                Limit = 1
            }
        );

        Assert.That(transResponse.CurrentPage.Items.Count, Is.EqualTo(1));

        // now get the full transaction information
        var transDetailResponse = await client.Reporting.Settlement.RetrieveTransactionAsync(
            new RetrieveTransactionSettlementRequest
            { TransactionId = transResponse.CurrentPage.Items[0].TransactionId ?? 0 }
        );

        Assert.Multiple(() =>
        {
            Assert.That(transDetailResponse.TransactionId, Is.EqualTo(525754319));
            Assert.That(transDetailResponse.Amount, Is.EqualTo(9934));
            Assert.That(transDetailResponse?.Card?.Type, Is.EqualTo("visa"));
        });
    }
}