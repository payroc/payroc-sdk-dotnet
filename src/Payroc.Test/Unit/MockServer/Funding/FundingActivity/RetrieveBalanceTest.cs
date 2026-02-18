using NUnit.Framework;
using Payroc.Funding.FundingActivity;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingActivity;

[TestFixture]
public class RetrieveBalanceTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "limit": 2,
              "count": 2,
              "hasMore": true,
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/funding-balance?before=4525644354&limit=2"
                },
                {
                  "rel": "next",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/funding-balance?after=9876543219&limit=2"
                }
              ],
              "data": [
                {
                  "merchantId": "4525644354",
                  "funds": 120000,
                  "pending": 50050,
                  "available": 69950,
                  "currency": "USD"
                },
                {
                  "merchantId": "9876543219",
                  "funds": 50000,
                  "pending": 0,
                  "available": 50000,
                  "currency": "USD"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-balance")
                    .WithParam("before", "2571")
                    .WithParam("after", "8516")
                    .WithParam("limit", "1")
                    .WithParam("merchantId", "4525644354")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingActivity.RetrieveBalanceAsync(
            new RetrieveBalanceFundingActivityRequest
            {
                Before = "2571",
                After = "8516",
                Limit = 1,
                MerchantId = "4525644354",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string mockResponse = """
            {
              "limit": 10,
              "count": 0,
              "hasMore": false,
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                },
                {
                  "rel": "next",
                  "method": "get",
                  "href": "<uri>"
                }
              ],
              "data": [
                {
                  "merchantId": "4525644354",
                  "funds": 120000,
                  "pending": 50050,
                  "available": 69950,
                  "currency": "USD"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-balance")
                    .WithParam("before", "2571")
                    .WithParam("after", "8516")
                    .WithParam("limit", "1")
                    .WithParam("merchantId", "4525644354")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingActivity.RetrieveBalanceAsync(
            new RetrieveBalanceFundingActivityRequest
            {
                Before = "2571",
                After = "8516",
                Limit = 1,
                MerchantId = "4525644354",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
