using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc.Core;
using Payroc.Funding.FundingActivity;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingActivity;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "limit": 10,
              "count": 10,
              "hasMore": true,
              "links": [
                {
                  "rel": "previous",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/funding-activity?before=11&limit=10&datefrom=2024-07-01&dateto=2024-07-03"
                },
                {
                  "rel": "next",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/funding-activity?after=20&limit=10&datefrom=2024-07-01&dateto=2024-07-03"
                }
              ],
              "data": [
                {
                  "id": 11,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Pizza Doe",
                  "description": "sales",
                  "amount": 4999,
                  "type": "credit",
                  "currency": "USD"
                },
                {
                  "id": 12,
                  "date": "2024-07-02T19:32:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Pizza Doe",
                  "description": "sales",
                  "amount": 3999,
                  "type": "credit",
                  "currency": "USD"
                },
                {
                  "id": 13,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Pizza Doe",
                  "description": "sales",
                  "amount": 3299,
                  "type": "credit",
                  "currency": "USD"
                },
                {
                  "id": 14,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Payroc",
                  "description": "Interchange Fees",
                  "amount": 50,
                  "type": "debit",
                  "currency": "USD"
                },
                {
                  "id": 15,
                  "date": "2024-07-02T09:10:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Pizza Doe",
                  "description": "sales",
                  "amount": 4999,
                  "type": "credit",
                  "currency": "USD"
                },
                {
                  "id": 16,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Doe Hot Dogs",
                  "recipient": "Pizza Doe",
                  "description": "Adjustment",
                  "amount": 750,
                  "type": "credit",
                  "currency": "USD"
                },
                {
                  "id": 17,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Doe Hot Dogs",
                  "recipient": "Payroc",
                  "description": "Interchange Fees",
                  "amount": 5,
                  "type": "debit",
                  "currency": "USD"
                },
                {
                  "id": 18,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Payroc",
                  "description": "Charge back",
                  "amount": 1000,
                  "type": "debit",
                  "currency": "USD"
                },
                {
                  "id": 19,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Pizza Doe",
                  "description": "sales",
                  "amount": 5999,
                  "type": "credit",
                  "currency": "USD"
                },
                {
                  "id": 20,
                  "date": "2024-07-02T17:00:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Pizza Doe",
                  "description": "payment",
                  "amount": 1000,
                  "type": "debit",
                  "currency": "USD"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-activity")
                    .WithParam("before", "2571")
                    .WithParam("after", "8516")
                    .WithParam("dateFrom", "2024-07-02")
                    .WithParam("dateTo", "2024-07-03")
                    .WithParam("merchantId", "4525644354")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingActivity.RetrieveAsync(
            new RetrieveFundingActivityRequest
            {
                Before = "2571",
                After = "8516",
                DateFrom = new DateOnly(2024, 7, 2),
                DateTo = new DateOnly(2024, 7, 3),
                MerchantId = "4525644354",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<RetrieveFundingActivityResponse>(mockResponse))
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
                  "id": 12345,
                  "date": "2024-07-02T15:30:00.000Z",
                  "merchant": "Pizza Doe",
                  "recipient": "Pizza Doe",
                  "description": "payment",
                  "amount": 4999,
                  "type": "credit",
                  "currency": "USD"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-activity")
                    .WithParam("before", "2571")
                    .WithParam("after", "8516")
                    .WithParam("dateFrom", "2024-07-02")
                    .WithParam("dateTo", "2024-07-03")
                    .WithParam("merchantId", "4525644354")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Funding.FundingActivity.RetrieveAsync(
            new RetrieveFundingActivityRequest
            {
                Before = "2571",
                After = "8516",
                DateFrom = new DateOnly(2024, 7, 2),
                DateTo = new DateOnly(2024, 7, 3),
                MerchantId = "4525644354",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<RetrieveFundingActivityResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
