using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Payments.PaymentLinks;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.PaymentLinks;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "merchantReference": "LinkRef6543",
              "order": {
                "charge": {
                  "currency": "AED",
                  "type": "prompt"
                }
              },
              "authType": "sale",
              "paymentMethods": [
                "card"
              ],
              "type": "multiUse"
            }
            """;

        const string mockResponse = """
            {
              "paymentLinkId": "JZURRJBUPS",
              "merchantReference": "LinkRef6543",
              "order": {
                "description": "Pie It Forward charitable trust donation",
                "charge": {
                  "currency": "AED",
                  "type": "prompt"
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
              "status": "active",
              "createdOn": "2024-07-02",
              "expiresOn": "2024-08-02",
              "credentialOnFile": {
                "tokenize": true,
                "mitAgreement": "unscheduled"
              },
              "type": "multiUse"
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

        var response = await Client.Payments.PaymentLinks.CreateAsync(
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CreatePaymentLinksResponse>(mockResponse))
                .UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "merchantReference": "LinkRef7654",
              "order": {
                "orderId": "OrderRef7654",
                "charge": {
                  "currency": "AED",
                  "type": "prompt"
                }
              },
              "authType": "sale",
              "paymentMethods": [
                "card"
              ],
              "expiresOn": "2024-08-02",
              "type": "singleUse"
            }
            """;

        const string mockResponse = """
            {
              "paymentLinkId": "JZURRJBUPS",
              "merchantReference": "LinkRef6543",
              "order": {
                "description": "Pie It Forward charitable trust donation",
                "charge": {
                  "currency": "AED",
                  "type": "prompt"
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
              "status": "active",
              "createdOn": "2024-07-02",
              "expiresOn": "2024-08-02",
              "credentialOnFile": {
                "tokenize": true,
                "mitAgreement": "unscheduled"
              },
              "type": "multiUse"
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

        var response = await Client.Payments.PaymentLinks.CreateAsync(
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
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CreatePaymentLinksResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
