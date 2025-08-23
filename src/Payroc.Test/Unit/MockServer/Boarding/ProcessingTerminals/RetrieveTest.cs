using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingTerminals;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingTerminals;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "processingTerminalId": "011234001",
              "status": "active",
              "timezone": "Pacific/Midway",
              "program": "Payroc Cloud",
              "gateway": {
                "gateway": "payroc",
                "terminalTemplateId": "Roc Services_DX8000"
              },
              "batchClosure": {
                "batchCloseTime": "23:40",
                "batchCloseType": "automatic"
              },
              "applicationSettings": {
                "invoiceNumberPrompt": true,
                "clerkPrompt": false
              },
              "features": {
                "tips": {
                  "tipPrompt": true,
                  "tipAdjust": true,
                  "enabled": "true"
                },
                "enhancedProcessing": {
                  "enabled": true,
                  "transactionDataLevel": "level2",
                  "shippingAddressMode": "fullAddress"
                },
                "ebt": {
                  "enabled": true,
                  "ebtType": "foodStamp",
                  "fnsNumber": "1234567890"
                },
                "pinDebitCashback": false,
                "recurringPayments": true,
                "paymentLinks": {
                  "enabled": true,
                  "logoUrl": "LogoPayLink",
                  "footerNotes": "FooterNotesPayLink"
                },
                "preAuthorizations": true,
                "offlinePayments": true
              },
              "taxes": [
                {
                  "taxRate": 1.1,
                  "taxLabel": "taxLabel"
                }
              ],
              "security": {
                "tokenization": true,
                "avsPrompt": true,
                "avsLevel": "fullAddress",
                "cvvPrompt": true
              },
              "receiptNotifications": {
                "emailReceipt": true,
                "smsReceipt": true
              },
              "devices": [
                {
                  "manufacturer": "manufacturer",
                  "model": "model",
                  "serialNumber": "serialNumber",
                  "communicationType": "bluetooth"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/processingTerminalId")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingTerminals.RetrieveAsync(
            new RetrieveProcessingTerminalsRequest { ProcessingTerminalId = "processingTerminalId" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ProcessingTerminal>(mockResponse)).UsingDefaults()
        );
    }
}
