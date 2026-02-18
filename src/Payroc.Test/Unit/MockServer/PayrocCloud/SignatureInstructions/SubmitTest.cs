using NUnit.Framework;
using Payroc.PayrocCloud.SignatureInstructions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.SignatureInstructions;

[TestFixture]
public class SubmitTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "processingTerminalId": "1234001"
            }
            """;

        const string mockResponse = """
            {
              "status": "inProgress",
              "errorMessage": "errorMessage",
              "link": {
                "rel": "self",
                "method": "GET",
                "href": "https://api.payroc.com/v1/signature-instructions/a37439165d134678a9100ebba3b29597"
              },
              "signatureInstructionId": "a37439165d134678a9100ebba3b29597"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/devices/1850010868/signature-instructions")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PayrocCloud.SignatureInstructions.SubmitAsync(
            new SignatureInstructionRequest
            {
                SerialNumber = "1850010868",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                ProcessingTerminalId = "1234001",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
