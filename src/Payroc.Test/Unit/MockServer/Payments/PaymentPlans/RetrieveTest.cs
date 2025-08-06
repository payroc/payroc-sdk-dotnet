using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.PaymentPlans;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.PaymentPlans;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
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
                    .WithPath(
                        "/processing-terminals/processingTerminalId/payment-plans/paymentPlanId"
                    )
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Payments.PaymentPlans.RetrieveAsync(
            new RetrievePaymentPlansRequest
            {
                ProcessingTerminalId = "processingTerminalId",
                PaymentPlanId = "paymentPlanId",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaymentPlan>(mockResponse)).UsingDefaults()
        );
    }
}
