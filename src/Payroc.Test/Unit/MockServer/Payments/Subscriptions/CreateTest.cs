using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.Subscriptions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.Subscriptions;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "subscriptionId": "SubRef7654",
              "paymentPlanId": "PlanRef8765",
              "paymentMethod": {
                "token": "1234567890123456789",
                "type": "secureToken"
              },
              "name": "Premium Club",
              "description": "Premium Club subscription",
              "setupOrder": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Initial setup fee for Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
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
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5
                    }
                  ]
                }
              },
              "startDate": "2024-07-02",
              "endDate": "2025-07-01",
              "length": 12,
              "pauseCollectionFor": 0,
              "customFields": [
                {
                  "name": "yourCustomField",
                  "value": "abc123"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "subscriptionId": "SubRef7654",
              "processingTerminalId": "1234001",
              "paymentPlan": {
                "paymentPlanId": "PlanRef8765",
                "name": "Monthly Premium Club subscription",
                "link": {
                  "rel": "self",
                  "method": "GET",
                  "href": "https://api.payroc.com/v1/processing-terminals/1234001/payment-plans/PlanRef8765"
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
              "name": "Premium Club",
              "description": "Premium Club subscription",
              "currency": "USD",
              "setupOrder": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Initial setup fee for Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5,
                      "amount": 190
                    }
                  ],
                  "surcharge": {
                    "bypass": false,
                    "amount": 217,
                    "percentage": 5
                  },
                  "convenienceFee": {
                    "amount": 25
                  }
                }
              },
              "recurringOrder": {
                "amount": 4999,
                "description": "Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5,
                      "amount": 190
                    }
                  ],
                  "surcharge": {
                    "bypass": false,
                    "amount": 217,
                    "percentage": 5
                  },
                  "convenienceFee": {
                    "amount": 25
                  }
                }
              },
              "currentState": {
                "status": "active",
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
                    .WithPath("/processing-terminals/processingTerminalId/subscriptions")
                    .WithHeader("Idempotency-Key", "Idempotency-Key")
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

        var response = await Client.Payments.Subscriptions.CreateAsync(
            new SubscriptionRequest
            {
                ProcessingTerminalId = "processingTerminalId",
                IdempotencyKey = "Idempotency-Key",
                SubscriptionId = "SubRef7654",
                PaymentPlanId = "PlanRef8765",
                PaymentMethod = new SubscriptionRequestPaymentMethod(
                    new SubscriptionRequestPaymentMethod.SecureToken(
                        new SecureTokenPayload { Token = "1234567890123456789" }
                    )
                ),
                Name = "Premium Club",
                Description = "Premium Club subscription",
                SetupOrder = new SubscriptionPaymentOrder
                {
                    OrderId = "OrderRef6543",
                    Amount = 4999,
                    Description = "Initial setup fee for Premium Club subscription",
                    Breakdown = new SubscriptionOrderBreakdown
                    {
                        Subtotal = 4347,
                        Taxes = new List<Tax>()
                        {
                            new Tax { Name = "Sales Tax", Rate = 5 },
                        },
                    },
                },
                RecurringOrder = new SubscriptionRecurringOrder
                {
                    Amount = 4999,
                    Description = "Monthly Premium Club subscription",
                    Breakdown = new SubscriptionOrderBreakdown
                    {
                        Subtotal = 4347,
                        Taxes = new List<Tax>()
                        {
                            new Tax { Name = "Sales Tax", Rate = 5 },
                        },
                    },
                },
                StartDate = new DateOnly(2024, 7, 2),
                EndDate = new DateOnly(2025, 7, 1),
                Length = 12,
                PauseCollectionFor = 0,
                CustomFields = new List<CustomField>()
                {
                    new CustomField { Name = "yourCustomField", Value = "abc123" },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Subscription>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "subscriptionId": "SubRef7654",
              "paymentPlanId": "PlanRef8765",
              "paymentMethod": {
                "token": "1234567890123456789",
                "type": "secureToken"
              },
              "name": "Premium Club",
              "description": "Premium Club subscription",
              "setupOrder": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Initial setup fee for Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
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
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5
                    }
                  ]
                }
              },
              "startDate": "2024-07-02",
              "endDate": "2025-07-01",
              "length": 12,
              "pauseCollectionFor": 0,
              "customFields": [
                {
                  "name": "yourCustomField",
                  "value": "abc123"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "subscriptionId": "SubRef7654",
              "processingTerminalId": "1234001",
              "paymentPlan": {
                "paymentPlanId": "PlanRef8765",
                "name": "Monthly Premium Club subscription",
                "link": {
                  "rel": "self",
                  "method": "GET",
                  "href": "https://api.payroc.com/v1/processing-terminals/1234001/payment-plans/PlanRef8765"
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
              "name": "Premium Club",
              "description": "Premium Club subscription",
              "currency": "USD",
              "setupOrder": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Initial setup fee for Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5,
                      "amount": 190
                    }
                  ],
                  "surcharge": {
                    "bypass": false,
                    "amount": 50,
                    "percentage": 2
                  },
                  "convenienceFee": {
                    "amount": 217
                  }
                }
              },
              "recurringOrder": {
                "amount": 4999,
                "description": "Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "taxes": [
                    {
                      "name": "Sales Tax",
                      "rate": 5,
                      "amount": 190
                    }
                  ],
                  "surcharge": {
                    "bypass": false,
                    "amount": 50,
                    "percentage": 2
                  },
                  "convenienceFee": {
                    "amount": 217
                  }
                }
              },
              "currentState": {
                "status": "active",
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
                    .WithPath("/processing-terminals/processingTerminalId/subscriptions")
                    .WithHeader("Idempotency-Key", "Idempotency-Key")
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

        var response = await Client.Payments.Subscriptions.CreateAsync(
            new SubscriptionRequest
            {
                ProcessingTerminalId = "processingTerminalId",
                IdempotencyKey = "Idempotency-Key",
                SubscriptionId = "SubRef7654",
                PaymentPlanId = "PlanRef8765",
                PaymentMethod = new SubscriptionRequestPaymentMethod(
                    new SubscriptionRequestPaymentMethod.SecureToken(
                        new SecureTokenPayload { Token = "1234567890123456789" }
                    )
                ),
                Name = "Premium Club",
                Description = "Premium Club subscription",
                SetupOrder = new SubscriptionPaymentOrder
                {
                    OrderId = "OrderRef6543",
                    Amount = 4999,
                    Description = "Initial setup fee for Premium Club subscription",
                    Breakdown = new SubscriptionOrderBreakdown
                    {
                        Subtotal = 4347,
                        Taxes = new List<Tax>()
                        {
                            new Tax { Name = "Sales Tax", Rate = 5 },
                        },
                    },
                },
                RecurringOrder = new SubscriptionRecurringOrder
                {
                    Amount = 4999,
                    Description = "Monthly Premium Club subscription",
                    Breakdown = new SubscriptionOrderBreakdown
                    {
                        Subtotal = 4347,
                        Taxes = new List<Tax>()
                        {
                            new Tax { Name = "Sales Tax", Rate = 5 },
                        },
                    },
                },
                StartDate = new DateOnly(2024, 7, 2),
                EndDate = new DateOnly(2025, 7, 1),
                Length = 12,
                PauseCollectionFor = 0,
                CustomFields = new List<CustomField>()
                {
                    new CustomField { Name = "yourCustomField", Value = "abc123" },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Subscription>(mockResponse)).UsingDefaults()
        );
    }
}
