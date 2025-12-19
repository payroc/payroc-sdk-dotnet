using Payroc.Reporting.Settlement;

namespace Payroc.TestFunctional.Reporting.Settlement;

[TestFixture, Category("Reporting.Authorization")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveAuthorizationTests
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
        var listAuthorizationsRequest = new ListReportingSettlementAuthorizationsRequest
        {
            BatchId = batchResponse.CurrentPage.Items[0].BatchId ?? 0,
            Date = new DateOnly(2025, 09, 14),
            Limit = 1
        };
        var listAuthorizationsResponse = await client.Reporting.Settlement.ListAuthorizationsAsync(listAuthorizationsRequest);
        var retrieveAuthorizationRequest = new RetrieveAuthorizationSettlementRequest
        {
            AuthorizationId = listAuthorizationsResponse.CurrentPage.Items[0].AuthorizationId ?? 0,
        };
        var retrieveAuthorizationResponse = await client.Reporting.Settlement.RetrieveAuthorizationAsync(retrieveAuthorizationRequest);

        Assert.That(retrieveAuthorizationResponse.AuthorizationId, Is.EqualTo(listAuthorizationsResponse.CurrentPage.Items[0].AuthorizationId));
        Assert.That(retrieveAuthorizationResponse.AuthorizationId, Is.EqualTo(303218454));
        Assert.That(retrieveAuthorizationResponse.PreauthorizationRequestAmount, Is.EqualTo(9934));
        Assert.That(retrieveAuthorizationResponse?.Card?.Type, Is.EqualTo("visa"));
    }
}
