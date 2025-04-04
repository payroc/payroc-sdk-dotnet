using Payroc.Boarding;
using Payroc.Core;
using Payroc.Funding;
using Payroc.Payments;
using Payroc.PayrocCloud;
using Payroc.Reporting;

namespace Payroc;

public partial class BasePayrocClient
{
    private readonly RawClient _client;

    public BasePayrocClient(string? token = null, ClientOptions? clientOptions = null)
    {
        var defaultHeaders = new Headers(
            new Dictionary<string, string>()
            {
                { "Authorization", $"Bearer {token}" },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "Payroc" },
                { "X-Fern-SDK-Version", Version.Current },
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
        _client = new RawClient(clientOptions);
        Payments = new PaymentsClient(_client);
        Funding = new FundingClient(_client);
        Boarding = new BoardingClient(_client);
        PayrocCloud = new PayrocCloudClient(_client);
        Reporting = new ReportingClient(_client);
    }

    public PaymentsClient Payments { get; init; }

    public FundingClient Funding { get; init; }

    public BoardingClient Boarding { get; init; }

    public PayrocCloudClient PayrocCloud { get; init; }

    public ReportingClient Reporting { get; init; }
}
