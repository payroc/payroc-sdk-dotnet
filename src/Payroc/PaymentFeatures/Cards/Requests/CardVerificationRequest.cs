using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.PaymentFeatures.Cards;

[Serializable]
public record CardVerificationRequest
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who requested to verify the card.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    /// <summary>
    /// Polymorphic object that contains payment details.
    /// </summary>
    [JsonPropertyName("card")]
    public required CardVerificationRequestCard Card { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
