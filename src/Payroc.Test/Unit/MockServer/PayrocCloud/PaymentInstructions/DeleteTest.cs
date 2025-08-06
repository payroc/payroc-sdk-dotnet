using NUnit.Framework;
using Payroc.PayrocCloud.PaymentInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.PaymentInstructions;

[TestFixture]
public class DeleteTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/payment-instructions/paymentInstructionId")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.PayrocCloud.PaymentInstructions.DeleteAsync(
                new DeletePaymentInstructionsRequest
                {
                    PaymentInstructionId = "paymentInstructionId",
                }
            )
        );
    }
}
