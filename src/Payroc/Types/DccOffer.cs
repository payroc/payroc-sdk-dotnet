using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the dynamic currency conversion (DCC) offer.
///
/// For more information about DCC, go to [Dynamic Currency Conversion](/knowledge/card-payments/dynamic-currency-conversion).
/// </summary>
[Serializable]
public record DccOffer : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the cardholder accepted DCC offer.
    /// </summary>
    [JsonAccess(JsonAccessType.WriteOnly)]
    [JsonPropertyName("accepted")]
    public bool? Accepted { get; set; }

    /// <summary>
    /// Unique identifier of the DCC offer.
    /// </summary>
    [JsonPropertyName("offerReference")]
    public string? OfferReference { get; set; }

    /// <summary>
    /// Amount in the cardholder’s currency in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("fxAmount")]
    public required long FxAmount { get; set; }

    /// <summary>
    /// Currency of the transaction in the card’s currency. The value for the currency follows the [ISO 4217](https://www.iso.org/iso-4217-currency-codes.html) standard.
    /// </summary>
    [JsonPropertyName("fxCurrency")]
    public required Currency FxCurrency { get; set; }

    /// <summary>
    /// Three-digit currency code for the card. This code follows the [ISO 4217](https://www.iso.org/iso-4217-currency-codes.html) standard.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("fxCurrencyCode")]
    public string? FxCurrencyCode { get; set; }

    /// <summary>
    /// Number of decimal places between the smallest currency unit and a whole currency unit.
    ///
    /// For example, for GBP, the smallest currency unit is 1p and it is equal to £0.01.
    /// If you use GBP, the value for **fxCurrencyExponent** is 2.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("fxCurrencyExponent")]
    public int? FxCurrencyExponent { get; set; }

    /// <summary>
    /// Foreign exchange rate for the card's currency.
    /// </summary>
    [JsonPropertyName("fxRate")]
    public required double FxRate { get; set; }

    /// <summary>
    /// Markup percentage rate that the DCC provider applies to the foreign exchange rate.
    /// </summary>
    [JsonPropertyName("markup")]
    public required double Markup { get; set; }

    /// <summary>
    /// Supporting text for the markup rate.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("markupText")]
    public string? MarkupText { get; set; }

    /// <summary>
    /// Name of the DCC provider.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("provider")]
    public string? Provider { get; set; }

    /// <summary>
    /// Source that the DCC provider used to get the foreign exchange rates.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("source")]
    public string? Source { get; set; }

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
