using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PayrocCloud.ClosedLoopReads;

[Serializable]
public record RetrieveClosedLoopReadsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the closed-loop read.
    /// </summary>
    [JsonIgnore]
    public required string ClosedLoopReadId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
