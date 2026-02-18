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
        var testDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-90));

        try
        {
            var authorizationsRequest = new ListReportingSettlementAuthorizationsRequest
            {
                Date = testDate,
            };
            _= await client.Reporting.Settlement.ListAuthorizationsAsync(authorizationsRequest);

            // Settlement data may not exist in UAT as it requires overnight batch processing
            Assert.Pass("API call succeeded without errors");
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"BadRequestError: {string.Join(",", ex?.Body?.Errors?.Select(i => i.Message) ?? [])}.");
        }
    }
}
