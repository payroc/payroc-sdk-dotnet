using NUnit.Framework;
using Payroc.PayrocCloud.ClosedLoopReads;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.ClosedLoopReads;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "processingTerminalId": "1234001",
              "closedLoopReadId": "KEO45MAC1U",
              "readDate": "2024-07-02",
              "data": {
                "cardType": "MiFareClassic",
                "uid": "04134ee21f1d80"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/closed-loop-reads/JDN4ILZB0T")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PayrocCloud.ClosedLoopReads.RetrieveAsync(
            new RetrieveClosedLoopReadsRequest { ClosedLoopReadId = "JDN4ILZB0T" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
