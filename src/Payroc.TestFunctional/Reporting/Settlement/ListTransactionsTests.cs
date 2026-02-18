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
        var testDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-90));

        try
        {
            var listTransactionsRequest = new ListReportingSettlementTransactionsRequest
            {
                Date = testDate,
                Limit = 1
            };
            _= await client.Reporting.Settlement.ListTransactionsAsync(listTransactionsRequest);
            
            // Settlement data may not exist in UAT as it requires overnight batch processing
            Assert.Pass("API call succeeded without errors");
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"BadRequestError: {string.Join(",", ex?.Body?.Errors?.Select(i => i.Message) ?? [])}.");
        }
    }
}
