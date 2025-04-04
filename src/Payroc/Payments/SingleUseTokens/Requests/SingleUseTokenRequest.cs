using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.SingleUseTokens;

public record SingleUseTokenRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Channel that the merchant used to receive the payment details.
    /// </summary>
    [JsonPropertyName("channel")]
    public required SingleUseTokenRequestChannel Channel { get; set; }

    /// <summary>
    /// Operator who initiated the request.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Object that contains information about the payment method to tokenize.
    /// </summary>
    [JsonPropertyName("source")]
    public required SingleUseTokenRequestSource Source { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
