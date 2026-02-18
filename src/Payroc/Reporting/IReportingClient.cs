using Payroc.Reporting.Settlement;

namespace Payroc.Reporting;

public partial interface IReportingClient
{
    public ISettlementClient Settlement { get; }
}
