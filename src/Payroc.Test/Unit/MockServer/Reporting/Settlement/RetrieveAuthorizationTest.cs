using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
public class RetrieveAuthorizationTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "authorizationId": 12345,
              "createdDate": "2024-01-30",
              "lastModifiedDate": "2024-01-30",
              "authorizationResponse": "activityCountLimitExceeded",
              "preauthorizationRequestAmount": 10000,
              "currency": "currency",
              "batch": {
                "batchId": 1234,
                "date": "2024-07-02",
                "cycle": "am",
                "link": {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              },
              "card": {
                "cardNumber": "453985******7062",
                "type": "visa",
                "cvvPresenceIndicator": true,
                "avsRequest": true,
                "avsResponse": "avsResponse"
              },
              "merchant": {
                "merchantId": "4525644354",
                "doingBusinessAs": "Pizza Doe",
                "processingAccountId": 38765,
                "link": {
                  "rel": "processingAccount",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/processing-accounts/38765"
                }
              },
              "transaction": {
                "transactionId": 12345,
                "type": "capture",
                "date": "2024-07-02",
                "entryMethod": "barcodeRead",
                "amount": 25000,
                "link": {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              }
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/authorizations/1").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Reporting.Settlement.RetrieveAuthorizationAsync(
            new RetrieveAuthorizationSettlementRequest { AuthorizationId = 1 }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Authorization>(mockResponse)).UsingDefaults()
        );
    }
}
