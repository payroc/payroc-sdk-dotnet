using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Refunds;

[Serializable]
public record RefundAdjustment
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the refund.
    /// </summary>
    [JsonIgnore]
    public required string RefundId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Operator who requested the adjustment to the refund.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Array of objects that contain information about the adjustments to the refund.
    /// </summary>
    [JsonPropertyName("adjustments")]
    public IEnumerable<RefundAdjustmentAdjustmentsItem> Adjustments { get; set; } =
        new List<RefundAdjustmentAdjustmentsItem>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
