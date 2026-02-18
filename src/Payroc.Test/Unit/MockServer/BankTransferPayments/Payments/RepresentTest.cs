using NUnit.Framework;
using Payroc;
using Payroc.BankTransferPayments.Payments;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.BankTransferPayments.Payments;

[TestFixture]
public class RepresentTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "paymentMethod": {
                "type": "ach",
                "nameOnAccount": "Shara Hazel Hopper",
                "accountNumber": "1234567890",
                "routingNumber": "123456789"
              }
            }
            """;

        const string mockResponse = """
            {
              "paymentId": "M2MJOG6O2Y",
              "processingTerminalId": "1234001",
              "order": {
                "orderId": "OrderRef6543",
                "description": "Large Pepperoni Pizza",
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
                      "rate": 5
                    }
                  ]
                }
              },
              "customer": {
                "notificationLanguage": "en",
                "contactMethods": [
                  {
                    "type": "email",
                    "value": "jane.doe@example.com"
                  }
                ]
              },
              "bankAccount": {
                "type": "ach",
                "secCode": "web",
                "nameOnAccount": "Sarah Hazel Hopper",
                "accountNumber": "123456789",
                "routingNumber": "123456789",
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
                }
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
                "status": "ready",
                "authorizedAmount": 4999,
                "currency": "USD",
                "responseCode": "A",
                "responseMessage": "NoError",
                "processorResponseCode": "0"
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
                    .WithPath("/bank-transfer-payments/M2MJOG6O2Y/represent")
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

        var response = await Client.BankTransferPayments.Payments.RepresentAsync(
            new Representment
            {
                PaymentId = "M2MJOG6O2Y",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                PaymentMethod = new RepresentmentPaymentMethod(
                    new Payroc.BankTransferPayments.Payments.RepresentmentPaymentMethod.Ach(
                        new AchPayload
                        {
                            NameOnAccount = "Shara Hazel Hopper",
                            AccountNumber = "1234567890",
                            RoutingNumber = "123456789",
                        }
                    )
                ),
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
