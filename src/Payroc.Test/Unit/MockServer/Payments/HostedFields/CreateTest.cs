using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.HostedFields;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.HostedFields;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "libVersion": "1.1.0.123456",
              "scenario": "payment"
            }
            """;

        const string mockResponse = """
            {
              "processingTerminalId": "1234001",
              "token": "abcdef1234567890abcdef1234567890",
              "expiresAt": "2025-07-02T13:30:00.000Z"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/hosted-fields-sessions")
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

        var response = await Client.Payments.HostedFields.CreateAsync(
            new HostedFieldsCreateSessionRequest
            {
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                LibVersion = "1.1.0.123456",
                Scenario = HostedFieldsCreateSessionRequestScenario.Payment,
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<HostedFieldsCreateSessionResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
