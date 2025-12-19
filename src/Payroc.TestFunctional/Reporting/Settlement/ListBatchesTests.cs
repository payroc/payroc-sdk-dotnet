using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListBatchesTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Generic;
        var request = new ListReportingSettlementBatchesRequest
        {
            Date = new DateOnly(2025, 09, 14)
        };
        
        var response =  await client.Reporting.Settlement.ListBatchesAsync(request);

        Assert.That(response.CurrentPage.Items.Count, Is.EqualTo(1));
    }
}
