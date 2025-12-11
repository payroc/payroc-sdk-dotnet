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
    /// Masked card number. Our gateway shows only the first six digits and the last four digits of the card number, for example, `500165******0000`.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public string? CardNumber { get; set; }

    /// <summary>
    /// Card type, for example, Visa.
    ///
    /// **Note:** If we can’t match a dispute to a transaction, we don’t return a type object.
    /// </summary>
    [JsonPropertyName("type")]
    public CardSummaryType? Type { get; set; }

    /// <summary>
    /// Indicates whether the cardholder provided the Card Verification Value (CVV).
    /// </summary>
    [JsonPropertyName("cvvPresenceIndicator")]
    public bool? CvvPresenceIndicator { get; set; }

    /// <summary>
    /// Indicates whether the merchant used the Address Verification Service (AVS) to verify the cardholder's address.
    /// </summary>
    [JsonPropertyName("avsRequest")]
    public bool? AvsRequest { get; set; }

    /// <summary>
    /// Response from the Address Verification Service (AVS).
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
