using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

public record GetBatchSettlementRequest
{
    /// <summary>
    /// Unique identifier of the batch.
    /// </summary>
    [JsonIgnore]
    public required int BatchId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
