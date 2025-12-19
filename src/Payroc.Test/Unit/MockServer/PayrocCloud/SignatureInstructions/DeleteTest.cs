using NUnit.Framework;
using Payroc.PayrocCloud.SignatureInstructions;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.PayrocCloud.SignatureInstructions;

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
                    .WithPath("/signature-instructions/a37439165d134678a9100ebba3b29597")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.PayrocCloud.SignatureInstructions.DeleteAsync(
                new DeleteSignatureInstructionsRequest
                {
                    SignatureInstructionId = "a37439165d134678a9100ebba3b29597",
                }
            )
        );
    }
}
