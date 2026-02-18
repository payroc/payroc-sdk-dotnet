using NUnit.Framework;
using Payroc;
using Payroc.PaymentLinks;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PaymentLinks;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "type": "multiUse",
              "merchantReference": "LinkRef6543",
              "order": {
                "charge": {
                  "type": "prompt",
                  "currency": "AED"
                }
              },
              "authType": "sale",
              "paymentMethods": [
                "card"
              ]
            }
            """;

        const string mockResponse = """
            {
              "type": "multiUse",
              "merchantReference": "LinkRef6543",
              "order": {
                "description": "Pie It Forward charitable trust donation",
                "charge": {
                  "type": "prompt",
                  "currency": "AED"
                }
              },
              "authType": "sale",
              "paymentMethods": [
                "card"
              ],
              "customLabels": [
                {
                  "element": "paymentButton",
                  "label": "SUPPORT US"
                }
              ],
              "assets": {
                "paymentUrl": "https://payments.payroc.com/merchant/pay-by-link?token=7c2fc08c-cb0e-44ba-8bcd-cf6de6eb3206",
                "paymentButton": "<a href=\"https://payments.payroc.com/merchant/pay-by-link?token=7c2fc08c-cb0e-44ba-8bcd-cf6de6eb3206\" \ntarget=\"_blank\" style=\"color: #ffffff; background-color: #6C7A89; font-size: 18px; font-family: Helvetica, Arial, sans-serif; \ntext-decoration: none; border-radius: 30px; padding: 14px 28px; display: inline-block;\">Pay Now</a>\n"
              },
              "expiresOn": "2024-08-02",
              "credentialOnFile": {
                "tokenize": true,
                "mitAgreement": "unscheduled"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/payment-links")
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

        var response = await Client.PaymentLinks.CreateAsync(
            new CreatePaymentLinksRequest
            {
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new CreatePaymentLinksRequestBody(
                    new CreatePaymentLinksRequestBody.MultiUse(
                        new MultiUsePaymentLink
                        {
                            MerchantReference = "LinkRef6543",
                            Order = new MultiUsePaymentLinkOrder
                            {
                                Charge = new MultiUsePaymentLinkOrderCharge(
                                    new MultiUsePaymentLinkOrderCharge.Prompt(
                                        new PromptPaymentLinkCharge { Currency = Currency.Aed }
                                    )
                                ),
                            },
                            AuthType = MultiUsePaymentLinkAuthType.Sale,
                            PaymentMethods = new List<MultiUsePaymentLinkPaymentMethodsItem>()
                            {
                                MultiUsePaymentLinkPaymentMethodsItem.Card,
                            },
                        }
                    )
                ),
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "type": "singleUse",
              "merchantReference": "LinkRef7654",
              "order": {
                "orderId": "OrderRef7654",
                "charge": {
                  "type": "prompt",
                  "currency": "AED"
                }
              },
              "authType": "sale",
              "paymentMethods": [
                "card"
              ],
              "expiresOn": "2024-08-02"
            }
            """;

        const string mockResponse = """
            {
              "type": "multiUse",
              "merchantReference": "LinkRef6543",
              "order": {
                "description": "Pie It Forward charitable trust donation",
                "charge": {
                  "type": "prompt",
                  "currency": "AED"
                }
              },
              "authType": "sale",
              "paymentMethods": [
                "card"
              ],
              "customLabels": [
                {
                  "element": "paymentButton",
                  "label": "SUPPORT US"
                }
              ],
              "assets": {
                "paymentUrl": "https://payments.payroc.com/merchant/pay-by-link?token=7c2fc08c-cb0e-44ba-8bcd-cf6de6eb3206",
                "paymentButton": "<a href=\"https://payments.payroc.com/merchant/pay-by-link?token=7c2fc08c-cb0e-44ba-8bcd-cf6de6eb3206\" \ntarget=\"_blank\" style=\"color: #ffffff; background-color: #6C7A89; font-size: 18px; font-family: Helvetica, Arial, sans-serif; \ntext-decoration: none; border-radius: 30px; padding: 14px 28px; display: inline-block;\">Pay Now</a>\n"
              },
              "expiresOn": "2024-08-02",
              "credentialOnFile": {
                "tokenize": true,
                "mitAgreement": "unscheduled"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-terminals/1234001/payment-links")
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

        var response = await Client.PaymentLinks.CreateAsync(
            new CreatePaymentLinksRequest
            {
                ProcessingTerminalId = "1234001",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new CreatePaymentLinksRequestBody(
                    new CreatePaymentLinksRequestBody.SingleUse(
                        new SingleUsePaymentLink
                        {
                            MerchantReference = "LinkRef7654",
                            Order = new SingleUsePaymentLinkOrder
                            {
                                OrderId = "OrderRef7654",
                                Charge = new SingleUsePaymentLinkOrderCharge(
                                    new SingleUsePaymentLinkOrderCharge.Prompt(
                                        new PromptPaymentLinkCharge { Currency = Currency.Aed }
                                    )
                                ),
                            },
                            AuthType = SingleUsePaymentLinkAuthType.Sale,
                            PaymentMethods = new List<SingleUsePaymentLinkPaymentMethodsItem>()
                            {
                                SingleUsePaymentLinkPaymentMethodsItem.Card,
                            },
                            ExpiresOn = new DateOnly(2024, 8, 2),
                        }
                    )
                ),
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
