using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
public class ListDisputesStatusesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            [
              {
                "disputeStatusId": 12345,
                "status": "prearbitrationInProcess",
                "statusDate": "2024-02-01"
              }
            ]
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/disputes/1/statuses")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Reporting.Settlement.ListDisputesStatusesAsync(
            new ListDisputesStatusesSettlementRequest { DisputeId = 1 }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<IEnumerable<DisputeStatus>>(mockResponse))
                .UsingDefaults()
        );
    }
}
