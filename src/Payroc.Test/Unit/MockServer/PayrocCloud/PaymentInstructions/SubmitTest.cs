using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.PayrocCloud.PaymentInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.PaymentInstructions;

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
                "amount": 4999,
                "currency": "USD"
              },
              "customizationOptions": {
                "entryMethod": "deviceRead"
              },
              "autoCapture": true
            }
            """;

        const string mockResponse = """
            {
              "status": "inProgress",
              "errorMessage": "errorMessage",
              "link": {
                "rel": "self",
                "method": "GET",
                "href": "https://api.payroc.com/v1/payment-instructions/a37439165d134678a9100ebba3b29597"
              },
              "paymentInstructionId": "a37439165d134678a9100ebba3b29597"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/devices/1850010868/payment-instructions")
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

        var response = await Client.PayrocCloud.PaymentInstructions.SubmitAsync(
            new PaymentInstructionRequest
            {
                SerialNumber = "1850010868",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Operator = "Jane",
                ProcessingTerminalId = "1234001",
                Order = new PaymentInstructionOrder
                {
                    OrderId = "OrderRef6543",
                    Amount = 4999,
                    Currency = Currency.Usd,
                },
                CustomizationOptions = new CustomizationOptions
                {
                    EntryMethod = CustomizationOptionsEntryMethod.DeviceRead,
                },
                AutoCapture = true,
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PaymentInstruction>(mockResponse)).UsingDefaults()
        );
    }
}
