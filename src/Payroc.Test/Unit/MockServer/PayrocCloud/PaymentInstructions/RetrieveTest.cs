using NUnit.Framework;
using Payroc.PayrocCloud.PaymentInstructions;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.PaymentInstructions;

[TestFixture]
public class RetrieveTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "status": "completed",
              "errorMessage": "errorMessage",
              "link": {
                "rel": "payment",
                "method": "GET",
                "href": "https://api.payroc.com/v1/payments/M2MJOG6O2Y"
              },
              "paymentInstructionId": "a37439165d134678a9100ebba3b29597"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/payment-instructions/e743a9165d134678a9100ebba3b29597")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.PayrocCloud.PaymentInstructions.RetrieveAsync(
            new RetrievePaymentInstructionsRequest
            {
                PaymentInstructionId = "e743a9165d134678a9100ebba3b29597",
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
