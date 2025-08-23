using NUnit.Framework;
using Payroc.Payments.SecureTokens;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Payments.SecureTokens;

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
                    .WithPath(
                        "/processing-terminals/processingTerminalId/secure-tokens/secureTokenId"
                    )
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Payments.SecureTokens.DeleteAsync(
                new DeleteSecureTokensRequest
                {
                    ProcessingTerminalId = "processingTerminalId",
                    SecureTokenId = "secureTokenId",
                }
            )
        );
    }
}
