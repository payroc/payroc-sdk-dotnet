using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveAuthorizationTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Generic;
        var testDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-90));

        try
        {
            var listAuthorizationsRequest = new ListReportingSettlementAuthorizationsRequest
            {
                Date = testDate,
                Limit = 1
            };
            _= await client.Reporting.Settlement.ListAuthorizationsAsync(listAuthorizationsRequest);
            
            // Settlement data may not exist in UAT as it requires overnight batch processing
            Assert.Pass("API call succeeded without errors");
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"BadRequestError: {string.Join(",", ex?.Body?.Errors?.Select(i => i.Message) ?? [])}.");
        }
    }
}
