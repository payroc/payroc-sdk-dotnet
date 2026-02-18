using NUnit.Framework;
using Payroc.Boarding.ProcessingTerminals;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingTerminals;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
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
                "batchCloseType": "automatic",
                "batchCloseTime": "23:40"
              },
              "applicationSettings": {
                "invoiceNumberPrompt": true,
                "clerkPrompt": false
              },
              "features": {
                "tips": {
                  "enabled": false
                },
                "enhancedProcessing": {
                  "enabled": true,
                  "transactionDataLevel": "level2",
                  "shippingAddressMode": "fullAddress"
                },
                "ebt": {
                  "enabled": true,
                  "ebtType": "foodStamp",
                  "fnsNumber": "3456789"
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
                  "taxRate": 6,
                  "taxLabel": "Sales Tax"
                }
              ],
              "security": {
                "tokenization": false,
                "avsPrompt": true,
                "avsLevel": "fullAddress",
                "cvvPrompt": true
              },
              "receiptNotifications": {
                "emailReceipt": true,
                "smsReceipt": false
              },
              "devices": [
                {
                  "manufacturer": "Ingenico",
                  "model": "Axium Dx4000 Tsys",
                  "serialNumber": "DX400-1234",
                  "communicationType": "bluetooth"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingTerminals.RetrieveAsync(
            new RetrieveProcessingTerminalsRequest { ProcessingTerminalId = "1234001" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
