using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Payments;

[Serializable]
public record Representment
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the payment.
    /// </summary>
    [JsonIgnore]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Polymorphic object that contains the customer's updated payment details.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`ach` - Automated Clearing House (ACH) details
    /// -	`secureToken` - Secure token details
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public RepresentmentPaymentMethod? PaymentMethod { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
