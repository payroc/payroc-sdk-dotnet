using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the card.
/// </summary>
[Serializable]
public record CardSummary : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Card number. We mask the number except for the first six digits and the last four digits.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public string? CardNumber { get; set; }

    /// <summary>
    /// Card type. If we can't match a dispute to a transaction, we don't return a 'type' object.
    /// </summary>
    [JsonPropertyName("type")]
    public CardSummaryType? Type { get; set; }

    /// <summary>
    /// Indicates if the cardholder provided the CVV.
    /// </summary>
    [JsonPropertyName("cvvPresenceIndicator")]
    public bool? CvvPresenceIndicator { get; set; }

    /// <summary>
    /// Indicates if the AVS was used to verify the cardholder's address.
    /// </summary>
    [JsonPropertyName("avsRequest")]
    public bool? AvsRequest { get; set; }

    /// <summary>
    /// Response from the AVS.
    /// </summary>
    [JsonPropertyName("avsResponse")]
    public string? AvsResponse { get; set; }

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
