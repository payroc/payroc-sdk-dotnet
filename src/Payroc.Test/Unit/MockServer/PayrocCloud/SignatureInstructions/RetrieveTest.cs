using NUnit.Framework;
using Payroc.PayrocCloud.SignatureInstructions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.SignatureInstructions;

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
              "signatureInstructionId": "a37439165d134678a9100ebba3b29597"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/signature-instructions/a37439165d134678a9100ebba3b29597")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PayrocCloud.SignatureInstructions.RetrieveAsync(
            new RetrieveSignatureInstructionsRequest
            {
                SignatureInstructionId = "a37439165d134678a9100ebba3b29597",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
