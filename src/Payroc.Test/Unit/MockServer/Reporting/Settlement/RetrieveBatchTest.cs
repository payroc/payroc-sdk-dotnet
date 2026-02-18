using NUnit.Framework;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
public class RetrieveBatchTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "batchId": 123,
              "date": "2024-07-02",
              "createdDate": "2024-07-02",
              "lastModifiedDate": "2024-07-02",
              "saleAmount": 1000000,
              "heldAmount": 1000000,
              "returnAmount": 1000000,
              "transactionCount": 1000000,
              "currency": "currency",
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

        var response = await Client.Reporting.Settlement.RetrieveBatchAsync(
            new RetrieveBatchSettlementRequest { BatchId = 1 }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
