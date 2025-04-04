using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record BankTransferRefund
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the refund.
    /// </summary>
    [JsonPropertyName("refundId")]
    public required string RefundId { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("order")]
    public required BankTransferRefundOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public BankTransferCustomer? Customer { get; set; }

    /// <summary>
    /// Object that contains information about the bank account.
    /// </summary>
    [JsonPropertyName("bankAccount")]
    public required BankTransferRefundBankAccount BankAccount { get; set; }

    [JsonPropertyName("payment")]
    public PaymentSummary? Payment { get; set; }

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
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
