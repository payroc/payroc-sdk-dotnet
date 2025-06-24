using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record DisputeCurrentStatus
{
    [JsonPropertyName("link")]
    public Link? Link { get; set; }

    /// <summary>
    /// Unique identifier of the dispute status.
    /// </summary>
    [JsonPropertyName("disputeStatusId")]
    public int? DisputeStatusId { get; set; }

    /// <summary>
    /// Status of the dispute.
    /// </summary>
    [JsonPropertyName("status")]
    public DisputeStatusStatus? Status { get; set; }

    /// <summary>
    /// Date that the status changed. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("statusDate")]
    public DateOnly? StatusDate { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
