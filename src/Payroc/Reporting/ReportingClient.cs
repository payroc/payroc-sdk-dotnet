using Payroc.Core;
using Payroc.Reporting.Settlement;

namespace Payroc.Reporting;

public partial class ReportingClient
{
    private RawClient _client;

    internal ReportingClient(RawClient client)
    {
        try
        {
            _client = client;
            Settlement = new SettlementClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public SettlementClient Settlement { get; }
}
