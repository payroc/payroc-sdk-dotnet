using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record SingleUseToken
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who initiated the request.
    /// </summary>
    [JsonPropertyName("operator")]
    [JsonAccess(JsonAccessType.WriteOnly)]
    public string? Operator { get; set; }

    /// <summary>
    /// Object that contains information about the customer's payment details.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    [JsonAccess(JsonAccessType.WriteOnly)]
    public SingleUseTokenPaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the payment details.
    /// **Note:** Merchants can use the token with other terminals linked to their account.
    /// </summary>
    [JsonPropertyName("token")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public required string Token { get; set; }

    /// <summary>
    /// Date and time that the token expires. We return this value in the ISO 8601 format.
    /// </summary>
    [JsonPropertyName("expiresAt")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public required DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Object that contains information about the payment method that we tokenized.
    /// </summary>
    [JsonPropertyName("source")]
    public required SingleUseTokenSource Source { get; set; }

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
