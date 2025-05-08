using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about plain-text swiped card data.
/// </summary>
public record PlainTextSwipedDataFormat
{
    [JsonPropertyName("device")]
    public required Device Device { get; set; }

    /// <summary>
    /// Customer’s card data from the swiped transaction.
    /// </summary>
    [JsonPropertyName("trackData")]
    public required string TrackData { get; set; }

    /// <summary>
    /// Indicates a technical issue with the ICC transaction that resulted in the terminal falling back to a magnetic stripe transaction.
    /// </summary>
    [JsonPropertyName("fallback")]
    public bool? Fallback { get; set; }

    /// <summary>
    /// Explains the reason for the fallback.
    /// </summary>
    [JsonPropertyName("fallbackReason")]
    public PlainTextSwipedDataFormatFallbackReason? FallbackReason { get; set; }

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
