using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the sale and the customer's bank details.
/// </summary>
public record BankTransferPayment
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the payment.
    /// </summary>
    [JsonPropertyName("paymentId")]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("order")]
    public required BankTransferPaymentOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public BankTransferCustomer? Customer { get; set; }

    /// <summary>
    /// Object that contains information about the bank account.
    /// </summary>
    [JsonPropertyName("bankAccount")]
    public required BankTransferPaymentBankAccount BankAccount { get; set; }

    /// <summary>
    /// List of refunds issued against the payment
    /// </summary>
    [JsonPropertyName("refunds")]
    public IEnumerable<RefundSummary>? Refunds { get; set; }

    /// <summary>
    /// List of returns issued against the payment
    /// </summary>
    [JsonPropertyName("returns")]
    public IEnumerable<BankTransferReturnSummary>? Returns { get; set; }

    [JsonPropertyName("representment")]
    public PaymentSummary? Representment { get; set; }

    [JsonPropertyName("transactionResult")]
    public required BankTransferResult TransactionResult { get; set; }

    /// <summary>
    /// Array of customField objects.
    /// </summary>
    [JsonPropertyName("customFields")]
    public IEnumerable<CustomField>? CustomFields { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
