using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.PaymentFeatures.Cards;

[Serializable]
public record FxRateInquiry
{
    /// <summary>
    /// Channel that the merchant used to receive payment details for the transaction.
    /// </summary>
    [JsonPropertyName("channel")]
    public required FxRateInquiryChannel Channel { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who ran the transaction.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Total amount of the transaction in the merchant’s currency. The value is in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("baseAmount")]
    public required long BaseAmount { get; set; }

    [JsonPropertyName("baseCurrency")]
    public required Currency BaseCurrency { get; set; }

    /// <summary>
    /// Polymorphic object that contains payment details.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`card` - Payment card details
    /// -	`secureToken` - Secure token details
    /// -	`digitalWallet` - Digital wallet details
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public required FxRateInquiryPaymentMethod PaymentMethod { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
