using NUnit.Framework;
using Payroc;
using Payroc.BankTransferPayments.Refunds;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.BankTransferPayments.Refunds;

[TestFixture]
public class ReversePaymentTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "paymentId": "M2MJOG6O2Y",
              "processingTerminalId": "1234001",
              "order": {
                "orderId": "OrderRef6543",
                "dateTime": "2024-07-02T15:30:00.000Z",
                "description": "Large Pepperoni Pizza",
                "amount": 4999,
                "currency": "USD",
                "breakdown": {
                  "subtotal": 4347,
                  "tip": {
                    "type": "percentage",
                    "amount": 500,
                    "percentage": 10
                  },
                  "taxes": [
                    {
                      "name": "VAT",
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
                    .WithPath("/bank-transfer-payments/M2MJOG6O2Y/reverse")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .UsingPost()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.BankTransferPayments.Refunds.ReversePaymentAsync(
            new ReversePaymentRefundsRequest
            {
                PaymentId = "M2MJOG6O2Y",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BankTransferPayment>(mockResponse)).UsingDefaults()
        );
    }
}
