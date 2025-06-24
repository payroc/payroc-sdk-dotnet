using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments;

[Serializable]
public record PaymentRequest
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Channel that the merchant used to receive the payment details.
    /// </summary>
    [JsonPropertyName("channel")]
    public required PaymentRequestChannel Channel { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who ran the transaction.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("order")]
    public required PaymentOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("ipAddress")]
    public IpAddress? IpAddress { get; set; }

    /// <summary>
    /// Object that contains information about the customer's payment details.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public required PaymentRequestPaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Object that contains information for an authentication check on the customer's payment details using the 3-D Secure protocol.
    /// </summary>
    [JsonPropertyName("threeDSecure")]
    public PaymentRequestThreeDSecure? ThreeDSecure { get; set; }

    [JsonPropertyName("credentialOnFile")]
    public SchemasCredentialOnFile? CredentialOnFile { get; set; }

    [JsonPropertyName("offlineProcessing")]
    public OfflineProcessing? OfflineProcessing { get; set; }

    /// <summary>
    /// Indicates if we should automatically capture the payment amount.
    ///
    /// - `true` - Run a sale and automatically capture the transaction.
    /// - `false`- Run a pre-authorization and capture the transaction later.
    ///
    /// **Note:** If you send `false` and the terminal doesn't support pre-authorization, we set the transaction's status to pending. The merchant must capture the transaction to take payment from the customer.
    /// </summary>
    [JsonPropertyName("autoCapture")]
    public bool? AutoCapture { get; set; }

    /// <summary>
    /// Indicates if we should immediately settle the sale transaction. The merchant cannot adjust the transaction if we immediately settle it.
    /// **Note:** If the value for **processAsSale** is `true`, the gateway ignores the value in **autoCapture**.
    /// </summary>
    [JsonPropertyName("processAsSale")]
    public bool? ProcessAsSale { get; set; }

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
