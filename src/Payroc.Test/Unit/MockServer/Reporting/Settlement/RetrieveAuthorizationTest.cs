using NUnit.Framework;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
public class RetrieveAuthorizationTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "authorizationId": 65,
              "createdDate": "2024-07-02",
              "lastModifiedDate": "2024-07-02",
              "authorizationResponse": "successful",
              "preauthorizationRequestAmount": 10000,
              "currency": "USD",
              "batch": {
                "batchId": 12,
                "date": "2024-07-02",
                "cycle": "am",
                "link": {
                  "rel": "batch",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/batches/12"
                }
              },
              "card": {
                "cardNumber": "453985******7062",
                "type": "visa",
                "cvvPresenceIndicator": true,
                "avsRequest": true,
                "avsResponse": "Y"
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
                "transactionId": 442233,
                "type": "capture",
                "date": "2024-07-02",
                "entryMethod": "swiped",
                "amount": 100,
                "link": {
                  "rel": "transaction",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/transactions/12345"
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
        JsonAssert.AreEqual(response, mockResponse);
    }
}
