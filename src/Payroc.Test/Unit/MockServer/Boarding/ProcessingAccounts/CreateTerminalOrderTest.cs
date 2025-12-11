using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class CreateTerminalOrderTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
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
              ]
            }
            """;

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
                        "href": "href",
                        "rel": "rel",
                        "method": "method"
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
              "createdDate": "2024-07-02T12:00:00.000Z",
              "lastModifiedDate": "2024-07-02T12:00:00.000Z"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/38765/terminal-orders")
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

        var response = await Client.Boarding.ProcessingAccounts.CreateTerminalOrderAsync(
            new CreateTerminalOrder
            {
                ProcessingAccountId = "38765",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                TrainingProvider = TrainingProvider.Payroc,
                Shipping = new CreateTerminalOrderShipping
                {
                    Preferences = new CreateTerminalOrderShippingPreferences
                    {
                        Method = CreateTerminalOrderShippingPreferencesMethod.NextDay,
                        SaturdayDelivery = true,
                    },
                    Address = new CreateTerminalOrderShippingAddress
                    {
                        RecipientName = "Recipient Name",
                        BusinessName = "Company Ltd",
                        AddressLine1 = "1 Example Ave.",
                        AddressLine2 = "Example Address Line 2",
                        City = "Chicago",
                        State = "Illinois",
                        PostalCode = "60056",
                        Email = "example@mail.com",
                        Phone = "2025550164",
                    },
                },
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Type = OrderItemType.Solution,
                        SolutionTemplateId = "Roc Services_DX8000",
                        SolutionQuantity = 1f,
                        DeviceCondition = OrderItemDeviceCondition.New,
                        SolutionSetup = new OrderItemSolutionSetup
                        {
                            Timezone = SchemasTimezone.AmericaChicago,
                            IndustryTemplateId = "Retail",
                            GatewaySettings = new OrderItemSolutionSetupGatewaySettings
                            {
                                MerchantPortfolioId = "Company Ltd",
                                MerchantTemplateId = "Company Ltd Merchant Template",
                                UserTemplateId = "Company Ltd User Template",
                                TerminalTemplateId = "Company Ltd Terminal Template",
                            },
                            ApplicationSettings = new OrderItemSolutionSetupApplicationSettings
                            {
                                ClerkPrompt = false,
                                Security = new OrderItemSolutionSetupApplicationSettingsSecurity
                                {
                                    RefundPassword = true,
                                    KeyedSalePassword = false,
                                    ReversalPassword = true,
                                },
                            },
                            DeviceSettings = new OrderItemSolutionSetupDeviceSettings
                            {
                                NumberOfMobileUsers = 2f,
                                CommunicationType =
                                    OrderItemSolutionSetupDeviceSettingsCommunicationType.Wifi,
                            },
                            BatchClosure = new OrderItemSolutionSetupBatchClosure(
                                new OrderItemSolutionSetupBatchClosure.Automatic(
                                    new AutomaticBatchClose()
                                )
                            ),
                            ReceiptNotifications = new OrderItemSolutionSetupReceiptNotifications
                            {
                                EmailReceipt = true,
                                SmsReceipt = false,
                            },
                            Taxes = new List<OrderItemSolutionSetupTaxesItem>()
                            {
                                new OrderItemSolutionSetupTaxesItem
                                {
                                    TaxRate = 6f,
                                    TaxLabel = "Sales Tax",
                                },
                            },
                            Tips = new OrderItemSolutionSetupTips { Enabled = false },
                            Tokenization = true,
                        },
                    },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<TerminalOrder>(mockResponse)).UsingDefaults()
        );
    }
}
