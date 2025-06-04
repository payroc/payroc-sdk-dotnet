using NUnit.Framework;
using Payroc;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Payroc.Test.Unit.MockServer;

[SetUpFixture]
public class BaseMockServerTest
{
    protected static WireMockServer Server { get; set; } = null!;

    protected static BasePayrocClient Client { get; set; } = null!;

    protected static RequestOptions RequestOptions { get; set; } = new();

    private void MockOAuthEndpoint()
    {
        const string requestJson = """
            {
              "client_id": "CLIENT_ID",
              "client_secret": "CLIENT_SECRET"
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
    }

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        // Start the WireMock server
        Server = WireMockServer.Start(
            new WireMockServerSettings { Logger = new WireMockConsoleLogger() }
        );

        // Initialize the Client
        Client = new BasePayrocClient(
            "API_KEY",
            clientOptions: new ClientOptions
            {
                Environment = new PayrocEnvironment
                {
                    Api = Server.Urls[0],
                    Identity = Server.Urls[0],
                },
                MaxRetries = 0,
            }
        );
        MockOAuthEndpoint();
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        Server.Stop();
        Server.Dispose();
    }
}
