using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc.Core;
using Payroc.Funding.FundingActivity;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingActivity;

[TestFixture]
public class RetrieveBalanceTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
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
                WireMock.RequestBuilders.Request.Create().WithPath("/funding-balance").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingActivity.RetrieveBalanceAsync(
            new RetrieveBalanceFundingActivityRequest()
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<RetrieveBalanceFundingActivityResponse>(mockResponse))
                .UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
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
                WireMock.RequestBuilders.Request.Create().WithPath("/funding-balance").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingActivity.RetrieveBalanceAsync(
            new RetrieveBalanceFundingActivityRequest()
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<RetrieveBalanceFundingActivityResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
