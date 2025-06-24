using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record GetDisputesStatusesSettlementRequest
{
    /// <summary>
    /// Unique identifier of the dispute.
    /// </summary>
    [JsonIgnore]
    public required int DisputeId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
