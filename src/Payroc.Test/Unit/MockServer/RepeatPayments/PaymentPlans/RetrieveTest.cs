using NUnit.Framework;
using Payroc.RepeatPayments.PaymentPlans;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.RepeatPayments.PaymentPlans;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
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
                    .WithPath("/processing-terminals/1234001/payment-plans/PlanRef8765")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.RepeatPayments.PaymentPlans.RetrieveAsync(
            new RetrievePaymentPlansRequest
            {
                ProcessingTerminalId = "1234001",
                PaymentPlanId = "PlanRef8765",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
