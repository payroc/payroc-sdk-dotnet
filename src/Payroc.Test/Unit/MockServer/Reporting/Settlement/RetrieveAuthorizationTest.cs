using NUnit.Framework;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
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
                "cycle": "am"
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
                "processingAccountId": 38765
              },
              "transaction": {
                "transactionId": 442233,
                "type": "capture",
                "date": "2024-07-02",
                "entryMethod": "swiped",
                "amount": 100
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
