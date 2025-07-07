using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the dynamic currency conversion (DCC) offer.
///
/// For more information about DCC, go to [Dynamic currency conversion](/knowledge/card-payments/dynamic-currency-conversion).
/// </summary>
[Serializable]
public record DccOffer
{
    /// <summary>
    /// Indicates if the cardholder accepted DCC offer.
    /// </summary>
    [JsonAccess(JsonAccessType.WriteOnly)]
    [JsonPropertyName("accepted")]
    public bool? Accepted { get; set; }

    /// <summary>
    /// Identifier of the DCC offer.
    /// </summary>
    [JsonPropertyName("offerReference")]
    public string? OfferReference { get; set; }

    /// <summary>
    /// Amount in the cardholder’s currency in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("fxAmount")]
    public required long FxAmount { get; set; }

    [JsonPropertyName("fxCurrency")]
    public required Currency FxCurrency { get; set; }

    /// <summary>
    /// Three-digit currency code for the cardholder’s account. This code follows the ISO 4217 standard.
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
    /// Foreign exchange rate for the currency.
    /// </summary>
    [JsonPropertyName("fxRate")]
    public required double FxRate { get; set; }

    /// <summary>
    /// Mark-up percentage rate that the DCC provider applies to the foreign exchange rate.
    /// </summary>
    [JsonPropertyName("markup")]
    public required double Markup { get; set; }

    /// <summary>
    /// Supporting text for the mark-up rate.
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
    /// Source that the DCC provider uses to get the foreign exchange rates.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("source")]
    public string? Source { get; set; }

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
