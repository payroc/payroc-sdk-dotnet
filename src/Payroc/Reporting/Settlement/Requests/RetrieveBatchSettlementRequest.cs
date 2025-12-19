using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record RetrieveBatchSettlementRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the batch.
    /// </summary>
    [JsonIgnore]
    public required int BatchId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
