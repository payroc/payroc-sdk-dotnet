using NUnit.Framework;
using Payroc.Reporting.Settlement;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Reporting.Settlement;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
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
                "processingAccountId": 38765
              },
              "settled": {
                "settledBy": "3rd party",
                "achDate": "2024-07-02",
                "achDepositId": 99
              },
              "batch": {
                "batchId": 12,
                "date": "2024-07-02",
                "cycle": "am"
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
                "avsResponseCode": ""
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
