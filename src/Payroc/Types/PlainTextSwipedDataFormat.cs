using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about plain-text swiped card data.
/// </summary>
[Serializable]
public record PlainTextSwipedDataFormat : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("device")]
    public required Device Device { get; set; }

    /// <summary>
    /// Customer’s card data from the swiped transaction.
    /// </summary>
    [JsonPropertyName("trackData")]
    public required string TrackData { get; set; }

    /// <summary>
    /// Indicates that this is a fallback transaction. For example, if there was a technical issue with the chip on the customer's card and the merchant then swiped the card.
    /// </summary>
    [JsonPropertyName("fallback")]
    public bool? Fallback { get; set; }

    /// <summary>
    /// Reason for the fallback.
    /// </summary>
    [JsonPropertyName("fallbackReason")]
    public PlainTextSwipedDataFormatFallbackReason? FallbackReason { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
