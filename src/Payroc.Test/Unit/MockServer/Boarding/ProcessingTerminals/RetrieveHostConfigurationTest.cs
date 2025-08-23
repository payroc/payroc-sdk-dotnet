using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingTerminals;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingTerminals;

[TestFixture]
public class RetrieveHostConfigurationTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "processingTerminalId": "0123451001",
              "processingAccountId": "23451",
              "configuration": {
                "merchant": {
                  "posMid": "123456789101",
                  "chainNumber": "222222",
                  "settlementAgent": "0001",
                  "abaNumber": "967854108",
                  "binNumber": "888888",
                  "agentBankNumber": "000001",
                  "reimbursementAttribute": "Z",
                  "locationNumber": "000001"
                },
                "terminal": {
                  "terminalId": "V500000",
                  "terminalNumber": "1111",
                  "authenticationCode": "A1B2C3",
                  "sharingGroups": "3E7HULY8NQWZG",
                  "motoAllowed": true,
                  "internetAllowed": true,
                  "cardPresentAllowed": true
                },
                "processor": "tsys"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/processingTerminalId/host-configurations")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingTerminals.RetrieveHostConfigurationAsync(
            new RetrieveHostConfigurationProcessingTerminalsRequest
            {
                ProcessingTerminalId = "processingTerminalId",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<HostConfiguration>(mockResponse)).UsingDefaults()
        );
    }
}
