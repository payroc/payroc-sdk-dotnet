using NUnit.Framework;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
public class RetrieveTransactionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "transactionId": 442233,
              "type": "capture",
              "date": "2024-07-02",
              "amount": 4999,
              "entryMethod": "ecommerce",
              "createdDate": "2024-07-02",
              "lastModifiedDate": "2024-07-02",
              "status": "paid",
              "cashbackAmount": 0,
              "interchange": {
                "basisPoint": 0,
                "transactionFee": 0
              },
              "currency": "USD",
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
              "settled": {
                "settledBy": "3rd party",
                "achDate": "2024-07-02",
                "achDepositId": 99,
                "link": {
                  "rel": "achDeposit",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/ach-deposits/99"
                }
              },
              "batch": {
                "batchId": 12,
                "date": "2024-07-02",
                "cycle": "am",
                "link": {
                  "rel": "batch",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/batches/12"
                }
              },
              "card": {
                "cardNumber": "453985******7062",
                "type": "visa",
                "cvvPresenceIndicator": true,
                "avsRequest": true,
                "avsResponse": ""
              },
              "authorization": {
                "authorizationId": 303101,
                "code": "A1B2C3",
                "amount": 4999,
                "avsResponseCode": "",
                "link": {
                  "rel": "authorization",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/authorizations/303101"
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
        JsonAssert.AreEqual(response, mockResponse);
    }
}
