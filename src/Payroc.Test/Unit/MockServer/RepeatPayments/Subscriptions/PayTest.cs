using NUnit.Framework;
using Payroc;
using Payroc.RepeatPayments.Subscriptions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.RepeatPayments.Subscriptions;

[TestFixture]
public class PayTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "operator": "Jane",
              "order": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Monthly Premium Club subscription"
              }
            }
            """;

        const string mockResponse = """
            {
              "subscriptionId": "SubRef7654",
              "processingTerminalId": "1234001",
              "payment": {
                "paymentId": "M2MJOG6O2Y",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "currency": "USD",
                "amount": 4999,
                "status": "ready",
                "responseCode": "A",
                "responseMessage": "Transaction approved",
                "link": {
                  "rel": "self",
                  "method": "GET",
                  "href": "https://api.payroc.com/v1/bank-transfer-payments/M2MJOG6O2Y"
                }
              },
              "secureToken": {
                "secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                "customerName": "Sarah Hazel Hopper",
                "token": "296753123456",
                "status": "notValidated",
                "link": {
                  "rel": "self",
                  "method": "GET",
                  "href": "https://api.payroc.com/v1/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
                }
              },
              "currentState": {
                "status": "active",
                "nextDueDate": "2024-08-02",
                "paidInvoices": 1,
                "outstandingInvoices": 2
              },
              "customFields": [
                {
                  "name": "yourCustomField",
                  "value": "abc123"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654/pay")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PayAsync(
            new SubscriptionPaymentRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Operator = "Jane",
                Order = new SubscriptionPaymentOrder
                {
                    OrderId = "OrderRef6543",
                    Amount = 4999,
                    Description = "Monthly Premium Club subscription",
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
