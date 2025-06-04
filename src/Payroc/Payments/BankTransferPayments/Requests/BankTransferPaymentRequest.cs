using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

public record BankTransferPaymentRequest
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("order")]
    public required BankTransferPaymentOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public BankTransferCustomer? Customer { get; set; }

    [JsonPropertyName("credentialOnFile")]
    public SchemasCredentialOnFile? CredentialOnFile { get; set; }

    /// <summary>
    /// Object that contains information about the customer's payment details.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public required BankTransferPaymentRequestPaymentMethod PaymentMethod { get; set; }

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
