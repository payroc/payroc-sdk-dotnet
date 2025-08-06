using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc.Core;
using Payroc.Payments.PaymentLinks;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.PaymentLinks;

[TestFixture]
public class DeactivateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
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
                    .WithPath("/payment-links/paymentLinkId/deactivate")
                    .UsingPost()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Payments.PaymentLinks.DeactivateAsync(
            new DeactivatePaymentLinksRequest { PaymentLinkId = "paymentLinkId" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<DeactivatePaymentLinksResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
