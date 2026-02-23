using NUnit.Framework;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
public class RetrieveAchDepositTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "achDepositId": 99,
              "associationDate": "2024-07-02",
              "achDate": "2024-07-02",
              "paymentDate": "2024-07-02",
              "transactions": 10,
              "sales": 50000,
              "returns": 10000,
              "dailyFees": 1000,
              "heldSales": 1000,
              "achAdjustment": 1000,
              "holdback": 1000,
              "reserveRelease": 500,
              "netAmount": 36500,
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
              "links": [
                {
                  "rel": "achDepositFees",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/ach-deposit-fees?achDepositId=99&merchantId=4525644354"
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/ach-deposits/1").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Reporting.Settlement.RetrieveAchDepositAsync(
            new RetrieveAchDepositSettlementRequest { AchDepositId = 1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
