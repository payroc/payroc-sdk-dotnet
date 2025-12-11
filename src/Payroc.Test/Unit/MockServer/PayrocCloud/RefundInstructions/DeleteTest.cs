using NUnit.Framework;
using Payroc.PayrocCloud.RefundInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.RefundInstructions;

[TestFixture]
public class DeleteTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/refund-instructions/a37439165d134678a9100ebba3b29597")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.PayrocCloud.RefundInstructions.DeleteAsync(
                new DeleteRefundInstructionsRequest
                {
                    RefundInstructionId = "a37439165d134678a9100ebba3b29597",
                }
            )
        );
    }
}
