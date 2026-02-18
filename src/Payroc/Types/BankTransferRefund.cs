using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record BankTransferRefund : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that our gateway assigned to the refund.
    /// </summary>
    [JsonPropertyName("refundId")]
    public required string RefundId { get; set; }

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
    /// Polymorphic object that contains bank account information.
    ///
    /// The value of the type field determines which variant you should use:
    /// -	`ach` - Automated Clearing House (ACH) details
    /// -	`pad` - Pre-authorized debit (PAD) details
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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
