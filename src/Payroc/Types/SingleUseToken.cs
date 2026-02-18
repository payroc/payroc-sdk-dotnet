using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record SingleUseToken : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("processingTerminalId")]
    public string? ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who initiated the request.
    /// </summary>
    [JsonAccess(JsonAccessType.WriteOnly)]
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Polymorphic object that contains payment card details.
    /// </summary>
    [JsonAccess(JsonAccessType.WriteOnly)]
    [JsonPropertyName("paymentMethod")]
    public SingleUseTokenPaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the payment details.
    /// **Note:** Merchants can use the token with other terminals linked to their account.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    /// <summary>
    /// Date and time that the token expires. We return this value in the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) format.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Polymorphic object that contains the payment method that we tokenized.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`ach` - Automated Clearing House (ACH) details
    /// -	`pad` - Pre-authorized debit (PAD) details
    /// -	`card` - Payment card details
    /// </summary>
    [JsonPropertyName("source")]
    public required SingleUseTokenSource Source { get; set; }

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
