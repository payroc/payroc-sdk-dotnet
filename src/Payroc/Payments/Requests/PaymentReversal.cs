using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

public record PaymentReversal
{
    /// <summary>
    /// Unique identifier of the payment that the merchant wants to retrieve.
    /// </summary>
    [JsonIgnore]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Operator who reversed the payment.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Amount of the payment that the merchant wants to reverse. The value is in the currency’s lowest denomination, for example, cents.
    /// **Note**: If the merchant doesn’t send an amount, we reverse the total amount of the transaction.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
