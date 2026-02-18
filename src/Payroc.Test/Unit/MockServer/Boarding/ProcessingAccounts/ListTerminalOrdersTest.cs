using System.Globalization;
using NUnit.Framework;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class ListTerminalOrdersTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            [
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
                        "processingTerminalId": "1234001",
                        "link": {
                          "href": "https://api.payroc.com/v1/processing-terminals/1234001",
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
                        "batchCloseType": "automatic",
                        "batchCloseTime": "23:40"
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
                ]
              }
            ]
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/38765/terminal-orders")
                    .WithParam("status", "open")
                    .WithParam("fromDateTime", "2024-09-08T12:00:00.000Z")
                    .WithParam("toDateTime", "2024-12-08T11:00:00.000Z")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingAccounts.ListTerminalOrdersAsync(
            new ListTerminalOrdersProcessingAccountsRequest
            {
                ProcessingAccountId = "38765",
                Status = ListTerminalOrdersProcessingAccountsRequestStatus.Open,
                FromDateTime = DateTime.Parse(
                    "2024-09-08T12:00:00.000Z",
                    null,
                    DateTimeStyles.AdjustToUniversal
                ),
                ToDateTime = DateTime.Parse(
                    "2024-12-08T11:00:00.000Z",
                    null,
                    DateTimeStyles.AdjustToUniversal
                ),
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
