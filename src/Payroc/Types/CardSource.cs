using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the customer's card details.
/// </summary>
public record CardSource
{
    /// <summary>
    /// Cardholder's name.
    /// </summary>
    [JsonPropertyName("cardholderName")]
    public required string CardholderName { get; set; }

    /// <summary>
    /// Primary account number of the customer's card.
    /// </summary>
    [JsonPropertyName("cardNumber")]
    public required string CardNumber { get; set; }

    /// <summary>
    /// Expiry date of the customer's card.
    /// </summary>
    [JsonPropertyName("expiryDate")]
    public string? ExpiryDate { get; set; }

    /// <summary>
    /// The card type.
    /// </summary>
    [JsonPropertyName("cardType")]
    public string? CardType { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    /// <summary>
    /// Indicates if the card is a debit card.
    /// </summary>
    [JsonPropertyName("debit")]
    public bool? Debit { get; set; }

    [JsonPropertyName("surcharging")]
    public Surcharging? Surcharging { get; set; }

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
