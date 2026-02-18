using NUnit.Framework;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.BankTransferPayments.Payments;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
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
                    .WithPath("/bank-transfer-payments/M2MJOG6O2Y")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.BankTransferPayments.Payments.RetrieveAsync(
            new Payroc.BankTransferPayments.Payments.RetrievePaymentsRequest
            {
                PaymentId = "M2MJOG6O2Y",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string mockResponse = """
            {
              "paymentId": "E29U8OU8Q4",
              "processingTerminalId": "1234001",
              "order": {
                "orderId": "OrderRef7654",
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
                    "rel": "self",
                    "method": "GET",
                    "href": "https://api.payroc.com/v1/bank-transfer-payments/M2MJOG6O2Y"
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
                "status": "declined",
                "authorizedAmount": 4999,
                "currency": "USD",
                "responseCode": "D",
                "responseMessage": "Payment Declined",
                "processorResponseCode": "R11"
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
                    .WithPath("/bank-transfer-payments/M2MJOG6O2Y")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.BankTransferPayments.Payments.RetrieveAsync(
            new Payroc.BankTransferPayments.Payments.RetrievePaymentsRequest
            {
                PaymentId = "M2MJOG6O2Y",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
