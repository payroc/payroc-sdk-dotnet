using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.CardPayments.Payments;

[Serializable]
public record PaymentAdjustment
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
    /// Operator who adjusted the payment.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Array of objects that contain information about the adjustments to the payment.
    /// </summary>
    [JsonPropertyName("adjustments")]
    public IEnumerable<PaymentAdjustmentAdjustmentsItem> Adjustments { get; set; } =
        new List<PaymentAdjustmentAdjustmentsItem>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
