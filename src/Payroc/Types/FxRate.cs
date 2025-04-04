using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Foreign exchange rate for the transaction.
/// </summary>
public record FxRate
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who ran the transaction.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Total amount of the transaction in the local currency. The value is in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("baseAmount")]
    public required long BaseAmount { get; set; }

    [JsonPropertyName("baseCurrency")]
    public required Currency BaseCurrency { get; set; }

    [JsonPropertyName("inquiryResult")]
    public required FxRateInquiryResult InquiryResult { get; set; }

    [JsonPropertyName("dccOffer")]
    public DccOffer? DccOffer { get; set; }

    [JsonPropertyName("cardInfo")]
    public required CardInfo CardInfo { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
