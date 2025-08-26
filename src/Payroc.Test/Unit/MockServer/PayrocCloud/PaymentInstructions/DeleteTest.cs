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
                    .WithPath("/payment-instructions/e743a9165d134678a9100ebba3b29597")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.PayrocCloud.PaymentInstructions.DeleteAsync(
                new DeletePaymentInstructionsRequest
                {
                    PaymentInstructionId = "e743a9165d134678a9100ebba3b29597",
                }
            )
        );
    }
}
