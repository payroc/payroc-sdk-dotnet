using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.PayrocCloud.RefundInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.RefundInstructions;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "status": "completed",
              "errorMessage": "errorMessage",
              "link": {
                "rel": "refund",
                "method": "GET",
                "href": "https://api.payroc.com/v1/refunds/CD3HN88U9F"
              },
              "refundInstructionId": "a37439165d134678a9100ebba3b29597"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/refund-instructions/a37439165d134678a9100ebba3b29597")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PayrocCloud.RefundInstructions.RetrieveAsync(
            new RetrieveRefundInstructionsRequest
            {
                RefundInstructionId = "a37439165d134678a9100ebba3b29597",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<RefundInstruction>(mockResponse)).UsingDefaults()
        );
    }
}
