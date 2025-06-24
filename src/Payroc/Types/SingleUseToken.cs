using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record SingleUseToken
{
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
    /// Object that contains information about the customer's payment details.
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
    /// Date and time that the token expires. We return this value in the ISO 8601 format.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Object that contains information about the payment method that we tokenized.
    /// </summary>
    [JsonPropertyName("source")]
    public required SingleUseTokenSource Source { get; set; }

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
