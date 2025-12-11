using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting;

[TestFixture, Category("Reporting.Settlement")]
[Parallelizable(ParallelScope.Fixtures)]
public class AuthorizationsReportingTests
{
    [Test]
    public async Task Transactions_Reporting_Lifecycle()
    {
        var client = GlobalFixture.Generic;

        // get the initial list of batches (should be 1)
        var batchResponse = await client.Reporting.Settlement.ListBatchesAsync(
            new ListReportingSettlementBatchesRequest
            {
                Date = new DateOnly(2025, 09, 14)
            }
        );

        var authResponse = await client.Reporting.Settlement.ListAuthorizationsAsync(
            new ListReportingSettlementAuthorizationsRequest
            {
                BatchId = batchResponse.CurrentPage.Items[0].BatchId ?? 0,
                Date = new DateOnly(2025, 09, 14),
                Limit = 1
            }
        );

        Assert.That(authResponse.CurrentPage.Items.Count, Is.EqualTo(1));

        // now get the full authorization information
        var authDetailResponse = await client.Reporting.Settlement.RetrieveAuthorizationAsync(
            new RetrieveAuthorizationSettlementRequest()
            { AuthorizationId = authResponse.CurrentPage.Items[0].AuthorizationId ?? 0 }
        );

        Assert.Multiple(() =>
        {
            Assert.That(authDetailResponse.AuthorizationId, Is.EqualTo(303218454));
            Assert.That(authDetailResponse.PreauthorizationRequestAmount, Is.EqualTo(9934));
            Assert.That(authDetailResponse?.Card?.Type, Is.EqualTo("visa"));
        });
    }
}
