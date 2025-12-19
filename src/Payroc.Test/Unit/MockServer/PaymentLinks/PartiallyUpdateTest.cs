using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.PaymentLinks;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PaymentLinks;

[TestFixture]
public class PartiallyUpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              }
            ]
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
                    .WithPath("/payment-links/JZURRJBUPS")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PaymentLinks.PartiallyUpdateAsync(
            new PartiallyUpdatePaymentLinksRequest
            {
                PaymentLinkId = "JZURRJBUPS",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PartiallyUpdatePaymentLinksResponse>(mockResponse))
                .UsingDefaults()
        );
    }

    [NUnit.Framework.Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            [
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "path": "path",
                "op": "remove"
              },
              {
                "from": "from",
                "path": "path",
                "op": "move"
              },
              {
                "from": "from",
                "path": "path",
                "op": "copy"
              },
              {
                "path": "path",
                "op": "remove"
              }
            ]
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
                    .WithPath("/payment-links/JZURRJBUPS")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PaymentLinks.PartiallyUpdateAsync(
            new PartiallyUpdatePaymentLinksRequest
            {
                PaymentLinkId = "JZURRJBUPS",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new List<PatchDocument>()
                {
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                    new PatchDocument(
                        new PatchDocument.Move(new PatchMove { From = "from", Path = "path" })
                    ),
                    new PatchDocument(
                        new PatchDocument.Copy(new PatchCopy { From = "from", Path = "path" })
                    ),
                    new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<PartiallyUpdatePaymentLinksResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
