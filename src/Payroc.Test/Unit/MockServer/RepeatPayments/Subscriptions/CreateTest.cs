using NUnit.Framework;
using Payroc;
using Payroc.RepeatPayments.Subscriptions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.RepeatPayments.Subscriptions;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "subscriptionId": "SubRef7654",
              "paymentPlanId": "PlanRef8765",
              "paymentMethod": {
                "type": "secureToken",
                "token": "1234567890123456789"
              },
              "name": "Premium Club",
              "description": "Premium Club subscription",
              "setupOrder": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Initial setup fee for Premium Club subscription"
              },
              "recurringOrder": {
                "amount": 4999,
                "description": "Monthly Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "taxes": [
                    {
                      "type": "rate",
                      "rate": 5,
                      "name": "Sales Tax"
                    }
                  ]
                }
              },
              "startDate": "2024-07-02",
              "endDate": "2025-07-01",
              "length": 12,
              "pauseCollectionFor": 0
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
                  "convenienceFee": {
                    "amount": 25
                  },
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
                "description": "Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "convenienceFee": {
                    "amount": 25
                  },
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
                    .WithPath("/processing-terminals/1234001/subscriptions")
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

        var response = await Client.RepeatPayments.Subscriptions.CreateAsync(
            new SubscriptionRequest
            {
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                SubscriptionId = "SubRef7654",
                PaymentPlanId = "PlanRef8765",
                PaymentMethod = new SubscriptionRequestPaymentMethod(
                    new Payroc.RepeatPayments.Subscriptions.SubscriptionRequestPaymentMethod.SecureToken(
                        new SecureTokenPayload { Token = "1234567890123456789" }
                    )
                ),
                Name = "Premium Club",
                Description = "Premium Club subscription",
                SetupOrder = new SubscriptionPaymentOrderRequest
                {
                    OrderId = "OrderRef6543",
                    Amount = 4999,
                    Description = "Initial setup fee for Premium Club subscription",
                },
                RecurringOrder = new SubscriptionRecurringOrderRequest
                {
                    Amount = 4999,
                    Description = "Monthly Premium Club subscription",
                    Breakdown = new SubscriptionOrderBreakdownRequest
                    {
                        Subtotal = 4347,
                        Taxes = new List<TaxRate>()
                        {
                            new TaxRate
                            {
                                Type = TaxRateType.Rate,
                                Rate = 5,
                                Name = "Sales Tax",
                            },
                        },
                    },
                },
                StartDate = new DateOnly(2024, 7, 2),
                EndDate = new DateOnly(2025, 7, 1),
                Length = 12,
                PauseCollectionFor = 0,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "subscriptionId": "SubRef7654",
              "paymentPlanId": "PlanRef8765",
              "paymentMethod": {
                "type": "secureToken",
                "token": "1234567890123456789"
              },
              "name": "Premium Club",
              "description": "Premium Club subscription",
              "setupOrder": {
                "orderId": "OrderRef6543",
                "amount": 4999,
                "description": "Initial setup fee for Premium Club subscription"
              },
              "recurringOrder": {
                "amount": 4999,
                "description": "Monthly Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "taxes": [
                    {
                      "type": "rate",
                      "rate": 5,
                      "name": "Sales Tax"
                    }
                  ]
                }
              },
              "startDate": "2024-07-02",
              "endDate": "2025-07-01",
              "length": 12,
              "pauseCollectionFor": 0
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
                  "convenienceFee": {
                    "amount": 217
                  },
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
                "description": "Premium Club subscription",
                "breakdown": {
                  "subtotal": 4347,
                  "convenienceFee": {
                    "amount": 217
                  },
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
                    .WithPath("/processing-terminals/1234001/subscriptions")
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

        var response = await Client.RepeatPayments.Subscriptions.CreateAsync(
            new SubscriptionRequest
            {
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                SubscriptionId = "SubRef7654",
                PaymentPlanId = "PlanRef8765",
                PaymentMethod = new SubscriptionRequestPaymentMethod(
                    new Payroc.RepeatPayments.Subscriptions.SubscriptionRequestPaymentMethod.SecureToken(
                        new SecureTokenPayload { Token = "1234567890123456789" }
                    )
                ),
                Name = "Premium Club",
                Description = "Premium Club subscription",
                SetupOrder = new SubscriptionPaymentOrderRequest
                {
                    OrderId = "OrderRef6543",
                    Amount = 4999,
                    Description = "Initial setup fee for Premium Club subscription",
                },
                RecurringOrder = new SubscriptionRecurringOrderRequest
                {
                    Amount = 4999,
                    Description = "Monthly Premium Club subscription",
                    Breakdown = new SubscriptionOrderBreakdownRequest
                    {
                        Subtotal = 4347,
                        Taxes = new List<TaxRate>()
                        {
                            new TaxRate
                            {
                                Type = TaxRateType.Rate,
                                Rate = 5,
                                Name = "Sales Tax",
                            },
                        },
                    },
                },
                StartDate = new DateOnly(2024, 7, 2),
                EndDate = new DateOnly(2025, 7, 1),
                Length = 12,
                PauseCollectionFor = 0,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
