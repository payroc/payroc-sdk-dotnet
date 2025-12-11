using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.RepeatPayments.PaymentPlans;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.RepeatPayments.PaymentPlans;

[TestFixture]
public class PartiallyUpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              }
            ]
            """;

        const string mockResponse = """
            {
              "paymentPlanId": "PlanRef8765",
              "processingTerminalId": "1234001",
              "name": "Premium Club",
              "description": "Monthly Premium Club subscription",
              "currency": "USD",
              "length": 12,
              "type": "automatic",
              "frequency": "monthly",
              "onUpdate": "continue",
              "onDelete": "complete",
              "customFieldNames": [
                "yourCustomField"
              ],
              "setupOrder": {
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
                      "rate": 5,
                      "amount": 190
                    }
                  ]
                }
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/payment-plans/PlanRef8765")
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

        var response = await Client.RepeatPayments.PaymentPlans.PartiallyUpdateAsync(
            new PartiallyUpdatePaymentPlansRequest
            {
                ProcessingTerminalId = "1234001",
                PaymentPlanId = "PlanRef8765",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaymentPlan>(mockResponse)).UsingDefaults()
        );
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "from": "from",
                "path": "path",
                "op": "move"
              },
              {
                "from": "from",
                "path": "path",
                "op": "copy"
              },
              {
                "path": "path",
                "op": "remove"
              }
            ]
            """;

        const string mockResponse = """
            {
              "paymentPlanId": "PlanRef8765",
              "processingTerminalId": "1234001",
              "name": "Premium Club",
              "description": "Monthly Premium Club subscription",
              "currency": "USD",
              "length": 12,
              "type": "automatic",
              "frequency": "monthly",
              "onUpdate": "continue",
              "onDelete": "complete",
              "customFieldNames": [
                "yourCustomField"
              ],
              "setupOrder": {
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
                      "rate": 5,
                      "amount": 190
                    }
                  ]
                }
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/payment-plans/PlanRef8765")
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

        var response = await Client.RepeatPayments.PaymentPlans.PartiallyUpdateAsync(
            new PartiallyUpdatePaymentPlansRequest
            {
                ProcessingTerminalId = "1234001",
                PaymentPlanId = "PlanRef8765",
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaymentPlan>(mockResponse)).UsingDefaults()
        );
    }
}
