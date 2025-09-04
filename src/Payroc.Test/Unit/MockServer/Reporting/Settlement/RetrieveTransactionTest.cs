using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
public class RetrieveTransactionTest : BaseMockServerTest
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
                "doingBusinessAs": "Pizza Doe",
                "processingAccountId": "38765",
                "link": {
                  "rel": "processingAccount",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/processing-accounts/38765"
                }
              },
              "settled": {
                "settledBy": "3rd party",
                "achDate": "2024-07-02",
                "achDepositId": 99,
                "link": {
                  "rel": "achDeposit",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/ach-deposits/99?merchantId=4525644354"
                }
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
                "cardNumber": "453985******7062",
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

        var response = await Client.Reporting.Settlement.RetrieveTransactionAsync(
            new RetrieveTransactionSettlementRequest { TransactionId = 1 }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Transaction>(mockResponse)).UsingDefaults()
        );
    }
}
