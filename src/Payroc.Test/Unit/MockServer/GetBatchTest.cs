using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Reporting.Settlement;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class GetBatchTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "batchId": 123,
              "date": "2024-07-02",
              "createdDate": "2024-07-02",
              "lastModifiedDate": "2024-07-02",
              "saleAmount": 1,
              "heldAmount": 1,
              "returnAmount": 1,
              "transactionCount": 1,
              "currency": "currency",
              "merchant": {
                "merchantId": "4525644354",
                "doingBusinessAs": "Doe Hot Dogs",
                "link": {
                  "rel": "merchant",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/merchants/4525644354"
                }
              },
              "links": [
                {
                  "rel": "transactions",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/transactions?batchId=123"
                },
                {
                  "rel": "authorizations",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/authorizations?batchId=123"
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/batches/1").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Reporting.Settlement.GetBatchAsync(
            new GetBatchSettlementRequest { BatchId = 1 },
            RequestOptions
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Batch>(mockResponse)).UsingPropertiesComparer()
        );
    }
}
