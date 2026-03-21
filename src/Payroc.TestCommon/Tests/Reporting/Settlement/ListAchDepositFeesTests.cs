using Payroc.Reporting.Settlement;

namespace Payroc.TestCommon.Tests.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListAchDepositFeesTests
{
    [Test]
    [Ignore("Data Issues: Internal Server Error 500: Could possibly be do to bad api key, or other server side issues")]
    public async Task Settlement_ListAchDepositFees_Success()
    {
        var client = GlobalFixture.Payments;
        var request = new ListReportingSettlementAchDepositFeesRequest()
        {
            Date = new DateOnly(2024, 07, 02),
            Limit = 1
        };

        var response = await client.Reporting.Settlement.ListAchDepositFeesAsync(request);

        Assert.That(response.CurrentPage.Items.Count, Is.EqualTo(1));
    }
}
