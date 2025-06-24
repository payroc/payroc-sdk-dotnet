using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the batch. If we can't match a dispute to a batch, we don't return 'batch' object.
/// </summary>
[Serializable]
public record BatchSummary
{
    /// <summary>
    /// Unique identifier of the batch.
    /// </summary>
    [JsonPropertyName("batchId")]
    public int? BatchId { get; set; }

    /// <summary>
    /// Date that the merchant submitted the batch.
    /// </summary>
    [JsonPropertyName("date")]
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Indicates the cycle that contains the batch.
    /// </summary>
    [JsonPropertyName("cycle")]
    public string? Cycle { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
