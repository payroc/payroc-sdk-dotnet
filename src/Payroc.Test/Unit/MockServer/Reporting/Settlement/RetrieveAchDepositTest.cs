using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;

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
              "transactions": 20,
              "sales": 5000,
              "returns": 100,
              "dailyFees": 100,
              "heldSales": 100,
              "achAdjustment": 100,
              "holdback": 100,
              "reserveRelease": 100,
              "netAmount": 5000,
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<AchDeposit>(mockResponse)).UsingDefaults()
        );
    }
}
