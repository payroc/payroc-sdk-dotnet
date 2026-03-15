using global::System.Net.Http;
using global::System.Text.Json;
using NUnit.Framework;
using Payroc.Core;
using WireMock.Server;
using SystemTask = global::System.Threading.Tasks.Task;
using WireMockRequest = WireMock.RequestBuilders.Request;
using WireMockResponse = WireMock.ResponseBuilders.Response;

namespace Payroc.Test.Core.Regression.RetryDisposedContent;

/// <summary>
/// Regression tests for the disposed StringContent bug in CloneRequestAsync.
///
/// Prior to the fix, CloneRequestAsync shallow-copied non-multipart content (e.g. StringContent),
/// causing the shared content to be disposed after the first retry. The second retry then threw
/// ObjectDisposedException when attempting to send the disposed content.
///
/// See README.md in this folder for full details.
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class RetryDisposedContentTests
{
    private WireMockServer _server;
    private HttpClient _httpClient;
    private RawClient _rawClient;
    private string _baseUrl;

    // ClientOptions.MaxRetries defaults to 2. Tests use this default directly to verify
    // correct behaviour at the SDK's out-of-the-box retry configuration.
    private const int DefaultMaxRetries = 2;

    [SetUp]
    public void SetUp()
    {
        _server = WireMockServer.Start();
        _baseUrl = _server.Url ?? "";
        _httpClient = new HttpClient { BaseAddress = new Uri(_baseUrl) };
        _rawClient = new RawClient(
            new ClientOptions { HttpClient = _httpClient }  // MaxRetries left at default (2)
        )
        {
            BaseRetryDelay = 0,
        };
    }

    [Test]
    public async SystemTask JsonBody_ShouldSurviveAllDefaultRetries_WithoutObjectDisposedException()
    {
        // Arrange: two consecutive 500s (exhausting both default retries), then success.
        // This is the exact scenario from the customer report — the second retry
        // was the one that threw ObjectDisposedException because the first retry's
        // using block disposed the shared StringContent.
        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("MultiRetryJson")
            .WillSetStateTo("FirstRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500));

        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("MultiRetryJson")
            .WhenStateIs("FirstRetry")
            .WillSetStateTo("SecondRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500));

        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("MultiRetryJson")
            .WhenStateIs("SecondRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(200).WithBody("Success"));

        var request = new JsonRequest
        {
            BaseUrl = _baseUrl,
            Method = HttpMethod.Post,
            Path = "/test",
            Body = new { merchantName = "Test Merchant", platformId = "PLAT-001" },
        };

        // Act
        var response = await _rawClient.SendRequestAsync(request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(200));

        var content = await response.Raw.Content.ReadAsStringAsync();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(content, Is.EqualTo("Success"));
            // 1 initial attempt + DefaultMaxRetries (2) retries = 3 total requests
            Assert.That(_server.LogEntries, Has.Count.EqualTo(1 + DefaultMaxRetries));

            // Verify all requests carried the correct JSON body
            foreach (var entry in _server.LogEntries)
            {
                Assert.That(entry.RequestMessage.Body, Is.Not.Null);
                using var json = JsonDocument.Parse(entry.RequestMessage.Body!);
                Assert.That(
                    json.RootElement.GetProperty("merchantName").GetString(),
                    Is.EqualTo("Test Merchant")
                );
                Assert.That(
                    json.RootElement.GetProperty("platformId").GetString(),
                    Is.EqualTo("PLAT-001")
                );
            }

            // Verify Content-Type header was preserved on the final retried request
            var finalEntry = _server.LogEntries.ElementAt(DefaultMaxRetries);
            var contentTypeHeader = finalEntry.RequestMessage.Headers?
                .FirstOrDefault(h => h.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase));
            Assert.That(contentTypeHeader?.Value, Is.Not.Null);
            Assert.That(
                string.Join(",", contentTypeHeader!.Value.Value),
                Does.Contain("application/json")
            );
        }
    }

    [Test]
    public async SystemTask JsonBody_ShouldReturnLastFailure_WhenAllDefaultRetriesExhausted()
    {
        // Arrange: three consecutive 500s — exhausts default retries, no success.
        // Verifies the SDK returns the final error response rather than throwing,
        // and that all DefaultMaxRetries + 1 attempts were made.
        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("ExhaustedRetries")
            .WillSetStateTo("FirstRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500).WithBody("Error1"));

        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("ExhaustedRetries")
            .WhenStateIs("FirstRetry")
            .WillSetStateTo("SecondRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500).WithBody("Error2"));

        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("ExhaustedRetries")
            .WhenStateIs("SecondRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500).WithBody("FinalError"));

        var request = new JsonRequest
        {
            BaseUrl = _baseUrl,
            Method = HttpMethod.Post,
            Path = "/test",
            Body = new { merchantName = "Test Merchant" },
        };

        // Act
        var response = await _rawClient.SendRequestAsync(request);

        // Assert
        var content = await response.Raw.Content.ReadAsStringAsync();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(response.StatusCode, Is.EqualTo(500));
            Assert.That(content, Is.EqualTo("FinalError"));
            // 1 initial attempt + DefaultMaxRetries (2) retries = 3 total requests
            Assert.That(_server.LogEntries, Has.Count.EqualTo(1 + DefaultMaxRetries));
        }
    }

    [Test]
    public async SystemTask MultipartBody_ShouldSurviveAllDefaultRetries_WithoutObjectDisposedException()
    {
        // Arrange: two consecutive 500s (exhausting both default retries), then success.
        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("MultiRetryMultipart")
            .WillSetStateTo("FirstRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500));

        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("MultiRetryMultipart")
            .WhenStateIs("FirstRetry")
            .WillSetStateTo("SecondRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(500));

        _server
            .Given(WireMockRequest.Create().WithPath("/test").UsingPost())
            .InScenario("MultiRetryMultipart")
            .WhenStateIs("SecondRetry")
            .RespondWith(WireMockResponse.Create().WithStatusCode(200).WithBody("Success"));

        var request = new Payroc.Core.MultipartFormRequest
        {
            BaseUrl = _baseUrl,
            Method = HttpMethod.Post,
            Path = "/test",
        };
        request.AddJsonPart("object", new { merchantName = "Test Merchant" });

        // Act
        var response = await _rawClient.SendRequestAsync(request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(200));

        var content = await response.Raw.Content.ReadAsStringAsync();
        using (Assert.EnterMultipleScope())
        {
            Assert.That(content, Is.EqualTo("Success"));
            // 1 initial attempt + DefaultMaxRetries (2) retries = 3 total requests
            Assert.That(_server.LogEntries, Has.Count.EqualTo(1 + DefaultMaxRetries));

            // Verify the final retried request still carried the multipart body
            var finalEntry = _server.LogEntries.ElementAt(DefaultMaxRetries);
            Assert.That(finalEntry.RequestMessage.Body, Does.Contain("\"merchantName\""));
            Assert.That(finalEntry.RequestMessage.Body, Does.Contain("\"Test Merchant\""));
        }
    }

    [TearDown]
    public void TearDown()
    {
        _server.Dispose();
        _httpClient.Dispose();
    }
}
