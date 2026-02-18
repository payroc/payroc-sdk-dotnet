using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.PaymentFeatures.Cards;

[Serializable]
public record BinLookup
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public string? ProcessingTerminalId { get; set; }

    /// <summary>
    /// Transaction amount that you send to check the surcharge amount. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Polymorphic object that contains payment details.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`card` - Payment card details
    /// -	`cardBin` - Bank identification number (BIN) of the payment card
    /// -	`secureToken` - Secure token details
    /// -	`digitalWallet` - Digital wallet details
    /// </summary>
    [JsonPropertyName("card")]
    public required BinLookupCard Card { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
