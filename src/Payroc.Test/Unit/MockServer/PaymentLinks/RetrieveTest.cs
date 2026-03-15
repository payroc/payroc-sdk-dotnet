using NUnit.Framework;
using Payroc.PaymentLinks;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PaymentLinks;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class RetrieveTest : BaseMockServerTest
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
                    .WithPath("/payment-links/JZURRJBUPS")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PaymentLinks.RetrieveAsync(
            new RetrievePaymentLinksRequest { PaymentLinkId = "JZURRJBUPS" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
