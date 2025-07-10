using Payroc.Auth;
using Payroc.Boarding;
using Payroc.Core;
using Payroc.Funding;
using Payroc.Notifications;
using Payroc.Payments;
using Payroc.PayrocCloud;
using Payroc.Reporting;

namespace Payroc;

public partial class BasePayrocClient
{
    private readonly RawClient _client;

    public BasePayrocClient(
        string? apiKey = null,
        ClientOptions? clientOptions = null
    )
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "Payroc" },
                { "X-Fern-SDK-Version", Version.Current },
                { "User-Agent", "Payroc/0.0.28" },
            }
        );
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }
        var tokenProvider = new OAuthTokenProvider(
            apiKey,
            new AuthClient(new RawClient(clientOptions.Clone()))
        );
        clientOptions.Headers["Authorization"] = new Func<string>(() =>
            tokenProvider.GetAccessTokenAsync().Result
        );
        _client = new RawClient(clientOptions);
        Payments = new PaymentsClient(_client);
        Notifications = new NotificationsClient(_client);
        Auth = new AuthClient(_client);
        Funding = new FundingClient(_client);
        Boarding = new BoardingClient(_client);
        PayrocCloud = new PayrocCloudClient(_client);
        Reporting = new ReportingClient(_client);
    }

    public PaymentsClient Payments { get; }

    public NotificationsClient Notifications { get; }

    public AuthClient Auth { get; }

    public FundingClient Funding { get; }

    public BoardingClient Boarding { get; }

    public PayrocCloudClient PayrocCloud { get; }

    public ReportingClient Reporting { get; }
}
