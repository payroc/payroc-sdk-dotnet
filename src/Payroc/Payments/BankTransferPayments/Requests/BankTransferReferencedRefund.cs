using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

[Serializable]
public record BankTransferReferencedRefund
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the payment.
    /// </summary>
    [JsonIgnore]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Total amount of the refund. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    /// <summary>
    /// Description of the refund.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
