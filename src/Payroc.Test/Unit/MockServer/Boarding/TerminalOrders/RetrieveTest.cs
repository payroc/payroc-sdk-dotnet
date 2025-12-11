using NUnit.Framework;
using Payroc;
using Payroc.Boarding.TerminalOrders;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.TerminalOrders;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "terminalOrderId": "12345",
              "status": "open",
              "trainingProvider": "payroc",
              "shipping": {
                "preferences": {
                  "method": "nextDay",
                  "saturdayDelivery": true
                },
                "address": {
                  "recipientName": "Recipient Name",
                  "businessName": "Company Ltd",
                  "addressLine1": "1 Example Ave.",
                  "addressLine2": "Example Address Line 2",
                  "city": "Chicago",
                  "state": "Illinois",
                  "postalCode": "60056",
                  "email": "example@mail.com",
                  "phone": "2025550164"
                }
              },
              "orderItems": [
                {
                  "links": [
                    {
                      "processingTerminalId": "processingTerminalId",
                      "link": {
                        "href": "https://api.payroc.com/v1/processing-terminals/38765",
                        "rel": "processingTerminal",
                        "method": "get"
                      }
                    }
                  ],
                  "type": "solution",
                  "solutionTemplateId": "Roc Services_DX8000",
                  "solutionQuantity": 1,
                  "deviceCondition": "new",
                  "solutionSetup": {
                    "timezone": "America/Chicago",
                    "industryTemplateId": "Retail",
                    "gatewaySettings": {
                      "merchantPortfolioId": "Company Ltd",
                      "merchantTemplateId": "Company Ltd Merchant Template",
                      "userTemplateId": "Company Ltd User Template",
                      "terminalTemplateId": "Company Ltd Terminal Template"
                    },
                    "applicationSettings": {
                      "clerkPrompt": false,
                      "security": {
                        "refundPassword": true,
                        "keyedSalePassword": false,
                        "reversalPassword": true
                      }
                    },
                    "deviceSettings": {
                      "numberOfMobileUsers": 2,
                      "communicationType": "wifi"
                    },
                    "batchClosure": {
                      "batchCloseTime": "23:40",
                      "batchCloseType": "automatic"
                    },
                    "receiptNotifications": {
                      "emailReceipt": true,
                      "smsReceipt": false
                    },
                    "taxes": [
                      {
                        "taxRate": 6,
                        "taxLabel": "Sales Tax"
                      }
                    ],
                    "tips": {
                      "enabled": false
                    },
                    "tokenization": true
                  }
                }
              ],
              "createdDate": "2020-09-08T12:00:00.000Z",
              "lastModifiedDate": "2020-09-09T12:00:00.000Z"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/terminal-orders/12345")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.TerminalOrders.RetrieveAsync(
            new RetrieveTerminalOrdersRequest { TerminalOrderId = "12345" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<TerminalOrder>(mockResponse)).UsingDefaults()
        );
    }
}
