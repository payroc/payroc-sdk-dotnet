using System.Net.Http;
using NUnit.Framework;
using Payroc;
using Payroc.CardPayments.Payments;
using WireMock.Server;
using WireMockRequest = WireMock.RequestBuilders.Request;
using WireMockResponse = WireMock.ResponseBuilders.Response;
using SystemTask = System.Threading.Tasks.Task;

namespace Payroc.TestNuget.Regression;

/// <summary>
/// Regression test for the disposed StringContent / retry bug, run against the published NuGet
/// package. Tests that a POST with a JSON body can survive the full default retry cycle (2 retries)
/// without throwing ObjectDisposedException.
///
/// Package under test: Payroc (see Payroc.TestNuget.csproj for version).
/// See src/Payroc.Test/Core/Regression/RetryDisposedContent/README.md for the full bug description.
/// </summary>
[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class RetryDisposedContentNugetTests
{
    // Default MaxRetries as documented in ClientOptions
    private const int DefaultMaxRetries = 2;

    private WireMockServer _server;
    private PayrocClient _client;

    private const string FakeTokenResponse =
        """{"access_token":"fake-token","token_type":"Bearer","expires_in":3600}""";

    [SetUp]
    public void SetUp()
    {
        _server = WireMockServer.Start();

        // Mock the identity/auth endpoint so PayrocClient can obtain a token
        _server
            .Given(WireMockRequest.Create().WithPath("/authorize").UsingPost())
            .RespondWith(
                WireMockResponse
                    .Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody(FakeTokenResponse)
            );

        var mockEnv = new PayrocEnvironment
        {
            Api = _server.Url!,
            Identity = _server.Url!,
        };

        _client = new PayrocClient(
            "fake-api-key",
            new ClientOptions
            {
                Environment = mockEnv,
                HttpClient = new HttpClient(),
                Telemetry = false,
            }
        );
    }

    private static PaymentRequest BuildPaymentRequest(string terminalId) =>
        Data.Get<PaymentRequest>(
        [
            (i => i.ProcessingTerminalId, terminalId),
            (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
        ]);

    private void SetUpRetryableServer(string scenario, int statusCode = 500)
    {
        // All DefaultMaxRetries + 1 responses return the same retryable status code.
        // We use a repeating WireMock mapping (no scenario state) so every request
        // to /payments gets the same response — simulating persistent server errors.
        _server
            .Given(WireMockRequest.Create().WithPath("/payments").UsingPost())
            .RespondWith(
                WireMockResponse
                    .Create()
                    .WithStatusCode(statusCode)
                    .WithHeader("Content-Type", "application/json")
                    .WithBody("""{"message":"Internal Server Error"}""")
            );
    }

    [Test]
    public async SystemTask CreatePayment_ShouldThrowPayrocApiException_NotObjectDisposedException_OnDefaultRetries()
    {
        // Arrange: persistent 500s — all DefaultMaxRetries + 1 attempts fail.
        // Prior to the fix, the second retry threw ObjectDisposedException because
        // CloneRequestAsync shallow-copied StringContent and the first retry's
        // using block disposed the shared instance.
        // After the fix, all retries complete cleanly and a PayrocApiException is thrown.
        SetUpRetryableServer("RetryPayment");

        // Act
        Exception? caughtException = null;
        try
        {
            await _client.CardPayments.Payments.CreateAsync(BuildPaymentRequest("1234001"));
        }
        catch (Exception ex)
        {
            caughtException = ex;
        }

        // Assert: must be a PayrocApiException (or subclass), NOT ObjectDisposedException
        Assert.That(caughtException, Is.Not.Null, "Expected an exception but none was thrown");
        Assert.That(
            caughtException,
            Is.InstanceOf<PayrocApiException>(),
            $"Expected PayrocApiException but got {caughtException!.GetType().Name}: {caughtException.Message}"
        );
        Assert.That(caughtException, Is.Not.InstanceOf<ObjectDisposedException>());

        // Verify: 1 initial attempt + DefaultMaxRetries retries = 3 total payment requests
        var paymentRequests = _server.LogEntries
            .Where(e => e.RequestMessage.Path == "/payments")
            .ToList();

        Assert.That(
            paymentRequests,
            Has.Count.EqualTo(1 + DefaultMaxRetries),
            $"Expected 1 initial + {DefaultMaxRetries} retries = {1 + DefaultMaxRetries} total payment requests"
        );
    }

    [Test]
    public async SystemTask CreatePayment_ShouldMakeAllDefaultRetryAttempts_WhenServerPersistentlyFails()
    {
        // Arrange: persistent 429s — all DefaultMaxRetries + 1 attempts fail.
        // Verifies that the retry loop runs to completion (all attempts made)
        // without the body being disposed mid-loop.
        SetUpRetryableServer("RateLimitRetry", statusCode: 429);

        // Act
        Exception? caughtException = null;
        try
        {
            await _client.CardPayments.Payments.CreateAsync(BuildPaymentRequest("1234001"));
        }
        catch (Exception ex)
        {
            caughtException = ex;
        }

        // Assert: a PayrocApiException is thrown (not ObjectDisposedException)
        Assert.That(caughtException, Is.Not.Null, "Expected an exception but none was thrown");
        Assert.That(
            caughtException,
            Is.InstanceOf<PayrocApiException>(),
            $"Expected PayrocApiException but got {caughtException!.GetType().Name}: {caughtException.Message}"
        );
        Assert.That(caughtException, Is.Not.InstanceOf<ObjectDisposedException>());

        // Verify: 1 initial attempt + DefaultMaxRetries retries = 3 total payment requests
        var paymentRequests = _server.LogEntries
            .Where(e => e.RequestMessage.Path == "/payments")
            .ToList();

        Assert.That(
            paymentRequests,
            Has.Count.EqualTo(1 + DefaultMaxRetries),
            $"Expected 1 initial + {DefaultMaxRetries} retries = {1 + DefaultMaxRetries} total payment requests"
        );
    }

    [TearDown]
    public void TearDown()
    {
        _server.Dispose();
    }
}
