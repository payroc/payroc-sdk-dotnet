using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc.Auth;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Auth;

[TestFixture]
public class RetrieveTokenTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "client_id": "client_id",
              "client_secret": "client_secret"
            }
            """;

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
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Auth.RetrieveTokenAsync(
            new RetrieveTokenAuthRequest
            {
                ApiKey = "x-api-key",
                ClientId = "client_id",
                ClientSecret = "client_secret",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<GetTokenResponse>(mockResponse)).UsingDefaults()
        );
    }
}
