using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.PayrocCloud.RefundInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.RefundInstructions;

[TestFixture]
public class SubmitTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "operator": "Jane",
              "processingTerminalId": "1234001",
              "order": {
                "orderId": "OrderRef6543",
                "description": "Refund for order OrderRef6543",
                "amount": 4999,
                "currency": "USD"
              },
              "customizationOptions": {
                "entryMethod": "manualEntry"
              }
            }
            """;

        const string mockResponse = """
            {
              "status": "inProgress",
              "errorMessage": "errorMessage",
              "link": {
                "rel": "self",
                "method": "GET",
                "href": "https://api.payroc.com/v1/refund-instructions/a37439165d134678a9100ebba3b29597"
              },
              "refundInstructionId": "a37439165d134678a9100ebba3b29597"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/devices/serialNumber/refund-instructions")
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

        var response = await Client.PayrocCloud.RefundInstructions.SubmitAsync(
            new RefundInstructionRequest
            {
                SerialNumber = "serialNumber",
                IdempotencyKey = "Idempotency-Key",
                Operator = "Jane",
                ProcessingTerminalId = "1234001",
                Order = new RefundInstructionOrder
                {
                    OrderId = "OrderRef6543",
                    Description = "Refund for order OrderRef6543",
                    Amount = 4999,
                    Currency = Currency.Usd,
                },
                CustomizationOptions = new CustomizationOptions
                {
                    EntryMethod = CustomizationOptionsEntryMethod.ManualEntry,
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<RefundInstruction>(mockResponse)).UsingDefaults()
        );
    }
}
