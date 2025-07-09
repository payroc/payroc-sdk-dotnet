using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.BankTransferPayments;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.BankTransferPayments;

[TestFixture]
public class RefundTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "amount": 4999,
              "description": "amount to refund"
            }
            """;

        const string mockResponse = """
            {
              "paymentId": "M2MJOG6O2Y",
              "processingTerminalId": "1234001",
              "order": {
                "orderId": "OrderRef6543",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "description": "Refund for order OrderRef6543",
                "amount": 4999,
                "currency": "USD",
                "breakdown": {
                  "subtotal": 4347,
                  "tip": {
                    "type": "percentage",
                    "amount": 435,
                    "percentage": 10
                  },
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5,
                      "amount": 217
                    }
                  ]
                }
              },
              "customer": {
                "notificationLanguage": "en",
                "contactMethods": [
                  {
                    "value": "jane.doe@example.com",
                    "type": "email"
                  }
                ]
              },
              "bankAccount": {
                "nameOnAccount": "Sarah Hazel Hopper",
                "accountNumber": "1234567890",
                "transitNumber": "76543",
                "institutionNumber": "543",
                "secureToken": {
                  "secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                  "customerName": "Sarah Hazel Hopper",
                  "token": "296753123456",
                  "status": "notValidated",
                  "link": {
                    "rel": "previous",
                    "method": "get",
                    "href": "<uri>"
                  }
                },
                "type": "pad"
              },
              "refunds": [
                {
                  "refundId": "CD3HN88U9F",
                  "dateTime": "2024-07-14T12:25:00.000Z",
                  "currency": "AED",
                  "amount": 4999,
                  "status": "ready",
                  "responseCode": "A",
                  "responseMessage": "Transaction refunded",
                  "link": {
                    "rel": "previous",
                    "method": "get",
                    "href": "<uri>"
                  }
                }
              ],
              "returns": [
                {
                  "paymentId": "M2MJOG6O2Y",
                  "date": "2024-07-02",
                  "returnCode": "R11",
                  "returnReason": "Customer advises not authorized",
                  "represented": false,
                  "link": {
                    "rel": "previous",
                    "method": "get",
                    "href": "<uri>"
                  }
                }
              ],
              "representment": {
                "paymentId": "M2MJOG6O2Y",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "currency": "AED",
                "amount": 4999,
                "status": "ready",
                "responseCode": "A",
                "responseMessage": "Transaction approved",
                "link": {
                  "rel": "previous",
                  "method": "get",
                  "href": "<uri>"
                }
              },
              "transactionResult": {
                "type": "payment",
                "status": "reversal",
                "authorizedAmount": 4999,
                "currency": "USD",
                "responseCode": "A",
                "responseMessage": "Payment Approved",
                "processorResponseCode": "A"
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
                    .WithPath("/bank-transfer-payments/M2MJOG6O2Y/refund")
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

        var response = await Client.Payments.BankTransferPayments.RefundAsync(
            new BankTransferReferencedRefund
            {
                PaymentId = "M2MJOG6O2Y",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Amount = 4999,
                Description = "amount to refund",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BankTransferPayment>(mockResponse)).UsingDefaults()
        );
    }
}
