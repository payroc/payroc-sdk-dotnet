using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the card.
/// </summary>
[Serializable]
public record CardInfo : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Card brand of the card, for example, Visa.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Masked card number. Our gateway shows only the first six digits and the last four digits of the card number, for example, 548010******5929.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public required string CardNumber { get; set; }

    /// <summary>
    /// Country of the issuing bank. The value for the country follows the [ISO-3166-1](https://www.iso.org/iso-3166-country-codes.html) standard.
    /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Indicates if the card is a debit card.
    /// </summary>
    [JsonPropertyName("debit")]
    public bool? Debit { get; set; }

    [JsonPropertyName("surcharging")]
    public Surcharging? Surcharging { get; set; }

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
