using NUnit.Framework;
using Payroc.RepeatPayments.Subscriptions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.RepeatPayments.Subscriptions;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class DeactivateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "subscriptionId": "SubRef7654",
              "processingTerminalId": "1234001",
              "paymentPlan": {
                "paymentPlanId": "PlanRef8765",
                "name": "Monthly Premium Club subscription"
              },
              "secureToken": {
                "secureTokenId": "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                "customerName": "Sarah Hazel Hopper",
                "token": "296753123456",
                "status": "notValidated"
              },
              "name": "Premium Club",
              "description": "Monthly Premium Club subscription",
              "currency": "USD",
              "setupOrder": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Initial setup fee for Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "surcharge": {
                    "bypass": false
                  },
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5
                    }
                  ]
                }
              },
              "recurringOrder": {
                "amount": 4999,
                "description": "Monthly Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "surcharge": {
                    "bypass": false
                  },
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5
                    }
                  ]
                }
              },
              "currentState": {
                "status": "cancelled",
                "nextDueDate": "2024-08-02",
                "paidInvoices": 0,
                "outstandingInvoices": 3
              },
              "startDate": "2024-07-02",
              "endDate": "2025-07-01",
              "length": 12,
              "type": "automatic",
              "frequency": "monthly",
              "pauseCollectionFor": 0,
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654/deactivate")
                    .UsingPost()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.DeactivateAsync(
            new DeactivateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
