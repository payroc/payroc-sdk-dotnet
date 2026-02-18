using NUnit.Framework;
using Payroc;
using Payroc.RepeatPayments.Subscriptions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.RepeatPayments.Subscriptions;

[TestFixture]
public class PartiallyUpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_3()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_4()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_5()
    {
        const string requestJson = """
            [
              {
                "op": "move",
                "from": "from",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(
                        new PatchDocument.Move(new PatchMove { From = "from", Path = "path" })
                    ),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_6()
    {
        const string requestJson = """
            [
              {
                "op": "copy",
                "from": "from",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(
                        new PatchDocument.Copy(new PatchCopy { From = "from", Path = "path" })
                    ),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_7()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_8()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "move",
                "from": "from",
                "path": "path"
              },
              {
                "op": "copy",
                "from": "from",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(
                        new PatchDocument.Move(new PatchMove { From = "from", Path = "path" })
                    ),
                    new PatchDocument(
                        new PatchDocument.Copy(new PatchCopy { From = "from", Path = "path" })
                    ),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_9()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_10()
    {
        const string requestJson = """
            [
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              },
              {
                "op": "remove",
                "path": "path"
              }
            ]
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
                    .WithPath("/processing-terminals/1234001/subscriptions/SubRef7654")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
            new PartiallyUpdateSubscriptionsRequest
            {
                ProcessingTerminalId = "1234001",
                SubscriptionId = "SubRef7654",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
