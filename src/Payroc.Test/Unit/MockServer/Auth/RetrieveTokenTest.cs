using NUnit.Framework;
using Payroc.Auth;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Auth;

[TestFixture]
public class RetrieveTokenTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "access_token": "access_token",
              "token_type": "token_type",
              "expires_in": 1,
              "scope": "scope"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/authorize")
                    .WithHeader("x-api-key", "x-api-key")
                    .UsingPost()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Auth.RetrieveTokenAsync(
            new RetrieveTokenAuthRequest { ApiKey = "x-api-key" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
