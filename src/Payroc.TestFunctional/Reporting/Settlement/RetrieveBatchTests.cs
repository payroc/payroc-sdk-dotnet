using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveBatchTests
{
    [Test]
    [Ignore("Data Issues: No settlement batches in UAT due to periodic data wipes.")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Generic;
        var testDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-90));

        try
        {
            var listBatchesRequest = new ListReportingSettlementBatchesRequest { Date = testDate };
            var listBatchesResponse = await client.Reporting.Settlement.ListBatchesAsync(listBatchesRequest);
            var batchId = listBatchesResponse.CurrentPage.Items.First().BatchId ?? 0;
            
            var retrieveBatchSettlementRequest = new RetrieveBatchSettlementRequest
            {
                BatchId = batchId
            };
            _= await client.Reporting.Settlement.RetrieveBatchAsync(retrieveBatchSettlementRequest);
            
            // Settlement data may not exist in UAT as it requires overnight batch processing
            Assert.Pass("API call succeeded without errors");
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"BadRequestError: {string.Join(",", ex?.Body?.Errors?.Select(i => i.Message) ?? [])}.");
        }
        catch (InvalidOperationException)
        {
            Assert.Fail("No batches found in UAT for the specified date range.");
        }
    }
}
