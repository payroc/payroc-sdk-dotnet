using NUnit.Framework;
using Payroc.Test.Unit.MockServer;
using Payroc.Tokenization.SecureTokens;

namespace Payroc.Test.Unit.MockServer.Tokenization.SecureTokens;

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
                    .WithPath(
                        "/processing-terminals/1234001/secure-tokens/MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa"
                    )
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Tokenization.SecureTokens.DeleteAsync(
                new DeleteSecureTokensRequest
                {
                    ProcessingTerminalId = "1234001",
                    SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
                }
            )
        );
    }
}
