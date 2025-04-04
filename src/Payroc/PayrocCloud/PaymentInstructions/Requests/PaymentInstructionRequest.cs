using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.PayrocCloud.PaymentInstructions;

public record PaymentInstructionRequest
{
    /// <summary>
    /// Serial number that identifies the merchant’s payment device.
    /// </summary>
    [JsonIgnore]
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Operator who initiated the request.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("order")]
    public required PaymentInstructionOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("ipAddress")]
    public IpAddress? IpAddress { get; set; }

    [JsonPropertyName("credentialOnFile")]
    public CredentialOnFile? CredentialOnFile { get; set; }

    [JsonPropertyName("customizationOptions")]
    public CustomizationOptions? CustomizationOptions { get; set; }

    /// <summary>
    /// Indicates if we should automatically capture the payment amount.
    ///
    /// - `true` - Run a sale and automatically capture the transaction.
    /// - `false`- Run a pre-authorization and capture the transaction later.
    ///
    /// **Note**: If you send `false` and the terminal doesn't support pre-authorization, we set the transaction's status to pending. The merchant must capture the transaction to take payment from the customer.
    /// </summary>
    [JsonPropertyName("autoCapture")]
    public bool? AutoCapture { get; set; }

    /// <summary>
    /// Indicates if we should immediately settle the sale transaction. The merchant cannot adjust the transaction if we immediately settle it.
    /// **Note**: If the value for **processAsSale** is `true`, the gateway ignores the value in **autoCapture**.
    /// </summary>
    [JsonPropertyName("processAsSale")]
    public bool? ProcessAsSale { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
