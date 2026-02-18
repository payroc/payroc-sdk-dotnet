using NUnit.Framework;
using Payroc;
using Payroc.RepeatPayments.PaymentPlans;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.RepeatPayments.PaymentPlans;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "paymentPlanId": "PlanRef8765",
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
              }
            }
            """;

        const string mockResponse = """
            {
              "paymentPlanId": "PlanRef8765",
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
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/payment-plans")
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

        var response = await Client.RepeatPayments.PaymentPlans.CreateAsync(
            new CreatePaymentPlansRequest
            {
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new PaymentPlan
                {
                    PaymentPlanId = "PlanRef8765",
                    Name = "Premium Club",
                    Description = "Monthly Premium Club subscription",
                    Currency = Currency.Usd,
                    Length = 12,
                    Type = PaymentPlanBaseType.Automatic,
                    Frequency = PaymentPlanBaseFrequency.Monthly,
                    OnUpdate = PaymentPlanBaseOnUpdate.Continue,
                    OnDelete = PaymentPlanBaseOnDelete.Complete,
                    CustomFieldNames = new List<string>() { "yourCustomField" },
                    SetupOrder = new PaymentPlanSetupOrder
                    {
                        Amount = 4999,
                        Description = "Initial setup fee for Premium Club subscription",
                        Breakdown = new PaymentPlanOrderBreakdown
                        {
                            Subtotal = 4347,
                            Taxes = new List<RetrievedTax>()
                            {
                                new RetrievedTax { Name = "Sales Tax", Rate = 5 },
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
                            Taxes = new List<RetrievedTax>()
                            {
                                new RetrievedTax { Name = "Sales Tax", Rate = 5 },
                            },
                        },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
