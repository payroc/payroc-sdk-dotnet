using NUnit.Framework;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.BankTransferPayments.Refunds;

[TestFixture]
public class ReverseRefundTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "refundId": "CD3HN88U9F",
              "processingTerminalId": "1234001",
              "order": {
                "orderId": "OrderRef6543",
                "description": "Refund for order OrderRef6543",
                "amount": 4999,
                "currency": "USD"
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
              "payment": {
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
                "type": "unreferencedRefund",
                "status": "reversal",
                "authorizedAmount": -4999,
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
                    .WithPath("/bank-transfer-refunds/CD3HN88U9F/reverse")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .UsingPost()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.BankTransferPayments.Refunds.ReverseRefundAsync(
            new Payroc.BankTransferPayments.Refunds.ReverseRefundRefundsRequest
            {
                RefundId = "CD3HN88U9F",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
