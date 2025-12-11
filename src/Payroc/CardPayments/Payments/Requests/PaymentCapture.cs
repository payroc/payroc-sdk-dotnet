using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.CardPayments.Payments;

[Serializable]
public record PaymentCapture
{
    /// <summary>
    /// Unique identifier of the payment that the merchant wants to retrieve.
    /// </summary>
    [JsonIgnore]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public string? ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who captured the payment.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Amount that the merchant wants to capture. The value is in the currency's lowest denomination, for example, cents.
    /// **Note:** If the merchant does not send an amount, we capture the total amount of the transaction.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    [JsonPropertyName("breakdown")]
    public ItemizedBreakdownRequest? Breakdown { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
