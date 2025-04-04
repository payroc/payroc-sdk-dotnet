using Payroc.Core;
using Payroc.Reporting.Settlement;

namespace Payroc.Reporting;

public partial class ReportingClient
{
    private RawClient _client;

    internal ReportingClient(RawClient client)
    {
        _client = client;
        Settlement = new SettlementClient(_client);
    }

    public SettlementClient Settlement { get; }
}
