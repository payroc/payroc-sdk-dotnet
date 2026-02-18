using NUnit.Framework;
using Payroc.PaymentLinks;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PaymentLinks;

[TestFixture]
public class DeactivateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
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
                    .WithPath("/payment-links/JZURRJBUPS/deactivate")
                    .UsingPost()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PaymentLinks.DeactivateAsync(
            new DeactivatePaymentLinksRequest { PaymentLinkId = "JZURRJBUPS" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
