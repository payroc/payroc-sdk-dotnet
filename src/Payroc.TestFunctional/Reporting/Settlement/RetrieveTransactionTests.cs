using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTransactionTests
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
        var listTransactionResponse = await client.Reporting.Settlement.ListTransactionsAsync(listTransactionsRequest);
        var retrieveTransactionRequest = new RetrieveTransactionSettlementRequest
        {
            TransactionId = listTransactionResponse.CurrentPage.Items[0].TransactionId ?? 0
        };
        
        var retrieveTransactionResponse = await client.Reporting.Settlement.RetrieveTransactionAsync(retrieveTransactionRequest);

        Assert.That(retrieveTransactionResponse.TransactionId, Is.EqualTo(listTransactionResponse.CurrentPage.Items[0].TransactionId));
        Assert.That(retrieveTransactionResponse.TransactionId, Is.EqualTo(525754319));
        Assert.That(retrieveTransactionResponse.Amount, Is.EqualTo(9934));
        Assert.That(retrieveTransactionResponse.Card?.Type, Is.EqualTo("visa")) ;
    }
}
