using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

[Serializable]
public record Representment
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
    /// Object that contains information about the customer's payment details.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public required RepresentmentPaymentMethod PaymentMethod { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
