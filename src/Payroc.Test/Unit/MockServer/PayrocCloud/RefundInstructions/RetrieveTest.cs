using NUnit.Framework;
using Payroc.PayrocCloud.RefundInstructions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.RefundInstructions;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "status": "completed",
              "errorMessage": "errorMessage",
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
        JsonAssert.AreEqual(response, mockResponse);
    }
}
