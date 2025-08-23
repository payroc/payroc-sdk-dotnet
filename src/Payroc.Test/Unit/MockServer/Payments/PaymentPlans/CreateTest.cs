using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.PaymentPlans;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.PaymentPlans;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "paymentPlanId": "PlanRef8765",
              "name": "Premium Club",
              "description": "Monthly Premium Club subscription",
              "currency": "USD",
              "setupOrder": {
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
              "length": 12,
              "type": "automatic",
              "frequency": "monthly",
              "onUpdate": "continue",
              "onDelete": "complete",
              "customFieldNames": [
                "yourCustomField"
              ]
            }
            """;

        const string mockResponse = """
            {
              "paymentPlanId": "PlanRef8765",
              "processingTerminalId": "1234001",
              "name": "Premium Club",
              "description": "Monthly Premium Club subscription",
              "currency": "USD",
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
              },
              "length": 12,
              "type": "automatic",
              "frequency": "monthly",
              "onUpdate": "continue",
              "onDelete": "complete",
              "customFieldNames": [
                "yourCustomField"
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/processingTerminalId/payment-plans")
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

        var response = await Client.Payments.PaymentPlans.CreateAsync(
            new CreatePaymentPlansRequest
            {
                ProcessingTerminalId = "processingTerminalId",
                IdempotencyKey = "Idempotency-Key",
                Body = new PaymentPlan
                {
                    PaymentPlanId = "PlanRef8765",
                    Name = "Premium Club",
                    Description = "Monthly Premium Club subscription",
                    Currency = Currency.Usd,
                    SetupOrder = new PaymentPlanSetupOrder
                    {
                        Amount = 4999,
                        Description = "Initial setup fee for Premium Club subscription",
                        Breakdown = new PaymentPlanOrderBreakdown
                        {
                            Subtotal = 4347,
                            Taxes = new List<Tax>()
                            {
                                new Tax { Name = "Sales Tax", Rate = 5 },
                            },
                        },
                    },
                    RecurringOrder = new PaymentPlanRecurringOrder
                    {
                        Amount = 4999,
                        Description = "Monthly Premium Club subscription",
                        Breakdown = new PaymentPlanOrderBreakdown
                        {
                            Subtotal = 4347,
                            Taxes = new List<Tax>()
                            {
                                new Tax { Name = "Sales Tax", Rate = 5 },
                            },
                        },
                    },
                    Length = 12,
                    Type = PaymentPlanType.Automatic,
                    Frequency = PaymentPlanFrequency.Monthly,
                    OnUpdate = PaymentPlanOnUpdate.Continue,
                    OnDelete = PaymentPlanOnDelete.Complete,
                    CustomFieldNames = new List<string>() { "yourCustomField" },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaymentPlan>(mockResponse)).UsingDefaults()
        );
    }
}
