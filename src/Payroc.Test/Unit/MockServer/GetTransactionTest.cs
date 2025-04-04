using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Reporting.Settlement;

namespace Payroc.Test.Unit.MockServer;

[TestFixture]
public class GetTransactionTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "transactionId": 442233,
              "type": "capture",
              "date": "2024-07-02",
              "amount": 4999,
              "entryMethod": "barcodeRead",
              "createdDate": "2024-07-02",
              "lastModifiedDate": "2024-07-02",
              "status": "fullSuspense",
              "cashbackAmount": 0,
              "interchange": {
                "basisPoint": 150,
                "transactionFee": 50
              },
              "currency": "USD",
              "merchant": {
                "merchantId": "4525644354",
                "doingBusinessAs": "Doe Hot Dogs",
                "link": {
                  "rel": "merchant",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/merchants/4525644354"
                }
              },
              "settled": {
                "settledBy": "Payroc",
                "achDate": "2024-07-02"
              },
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
                "cardNumber": "123456**********4124",
                "type": "visa",
                "cvvPresenceIndicator": true,
                "avsRequest": true,
                "avsResponse": "avsResponse"
              },
              "authorization": {
                "authorizationId": 303101,
                "code": "A1B2C3",
                "amount": 4999,
                "avsResponseCode": "Y",
                "link": {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/transactions/1").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Reporting.Settlement.GetTransactionAsync(
            new GetTransactionSettlementRequest { TransactionId = 1 },
            RequestOptions
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Transaction>(mockResponse)).UsingPropertiesComparer()
        );
    }
}
