using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListAchDepositTests
{
    [Test]
    [Ignore("Data Issues: Internal Server Error 500: Could possibly be do to bad api key, or other server side issues")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Generic;
        var request = new ListReportingSettlementAchDepositsRequest
        {
            Date = new DateOnly(2024, 07, 02),
            Limit = 1
        };
        var response = await client.Reporting.Settlement.ListAchDepositsAsync(request);
        _ = await client.Reporting.Settlement.ListAchDepositsAsync(request);
        _ = await client.Reporting.Settlement.ListAchDepositsAsync(request);
        var listAchFeesRequest = new ListReportingSettlementAchDepositFeesRequest
        {
            AchDepositId = response.CurrentPage.Items.FirstOrDefault()?.AchDepositId ?? 0,
            Limit = 1
        };
        
        var listAchFeesResponse = await client.Reporting.Settlement.ListAchDepositFeesAsync(listAchFeesRequest);
        
        Assert.That(listAchFeesResponse.CurrentPage.Items.Count, Is.GreaterThanOrEqualTo(2));
    }
}
