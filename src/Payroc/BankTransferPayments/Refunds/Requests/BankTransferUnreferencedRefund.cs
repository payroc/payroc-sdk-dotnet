using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Refunds;

[Serializable]
public record BankTransferUnreferencedRefund
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

    [JsonPropertyName("order")]
    public required BankTransferRefundOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public BankTransferCustomer? Customer { get; set; }

    /// <summary>
    /// Polymorphic object that contains payment details for the refund.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`ach` - Automated Clearing House (ACH) details
    /// -	`secureToken` - Secure token details
    /// </summary>
    [JsonPropertyName("refundMethod")]
    public required BankTransferUnreferencedRefundRefundMethod RefundMethod { get; set; }

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
