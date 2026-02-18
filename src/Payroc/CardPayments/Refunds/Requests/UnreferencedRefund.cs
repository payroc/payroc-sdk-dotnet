using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.CardPayments.Refunds;

[Serializable]
public record UnreferencedRefund
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Channel that the merchant used to request the refund.
    /// </summary>
    [JsonPropertyName("channel")]
    public required UnreferencedRefundChannel Channel { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who initiated the request.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("order")]
    public required RefundOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("ipAddress")]
    public IpAddress? IpAddress { get; set; }

    /// <summary>
    /// Polymorphic object that contains information about the payment method that the merchant uses to refund the customer.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`card` - Payment card details
    /// -	`secureToken` - Secure token details
    /// </summary>
    [JsonPropertyName("refundMethod")]
    public required UnreferencedRefundRefundMethod RefundMethod { get; set; }

    /// <summary>
    /// Array of customField objects.
    /// </summary>
    [JsonPropertyName("customFields")]
    public IEnumerable<CustomField>? CustomFields { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
