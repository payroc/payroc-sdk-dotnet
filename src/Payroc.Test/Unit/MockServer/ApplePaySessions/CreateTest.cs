using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.ApplePaySessions;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "appleDomainId": "DUHDZJHGYY",
              "appleValidationUrl": "https://apple-pay-gateway.apple.com/paymentservices/startSession"
            }
            """;

        const string mockResponse = """
            {
              "startSessionResponse": "{\n  \"epochTimestamp\": 1736264582447,\n  \"expiresAt\": 1736268182447,\n  \"merchantSessionIdentifier\": \"SSHE464E2B91B714F18BFD19D46D0F582BF_916523AAED1343F5BC5815E12BEE9250AFFDC1A17C46B0DE5A943F0F94927C24\",\n  \"nonce\": \"e5775127\",\n  \"merchantIdentifier\": \"BFB110EE83BE2AF4AA7468926C926CCFC57F4A541CCE6E7F3BEFD05002ECE503\",\n  \"domainName\": \"store.com\",\n  \"displayName\": \"Store One\",\n  \"signature\": \"a1b1c012345678a000b000c0012345d0e0f010g10061a031i001j071k0a1b0c1d0e1234567890120f1g0h1i0j1k0a1b0123451c012d0e1f0g1h0i1j123k1a1b1c1d1e1f1g123h1i1j1k1a1b1c1d1e1f1g123h123i1j123k12340a120a12345b012c0123012d0d1e0f1g0h1i123j123k10000\",\n  \"operationalAnalyticsIdentifier\": \"Store One:BFB110EE83BE2AF4AA7468926C926CCFC57F4A541CCE6E7F3BEFD05002ECE503\",\n  \"retries\": 0,\n  \"pspId\": \"17D4AAA8D9357D26D771ABA0DAA0B9D3BB462AD1585492E1FE688AF8BB9558E5\"\n}\n"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/apple-pay-sessions")
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

        var response = await Client.ApplePaySessions.CreateAsync(
            new Payroc.ApplePaySessions.ApplePaySessions
            {
                ProcessingTerminalId = "1234001",
                AppleDomainId = "DUHDZJHGYY",
                AppleValidationUrl =
                    "https://apple-pay-gateway.apple.com/paymentservices/startSession",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ApplePayResponseSession>(mockResponse)).UsingDefaults()
        );
    }
}
