using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the sale and the customer's bank details.
/// </summary>
[Serializable]
public record BankTransferPayment : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the payment.
    /// </summary>
    [JsonPropertyName("paymentId")]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
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
    /// List of refunds issued against the payment.
    /// </summary>
    [JsonPropertyName("refunds")]
    public IEnumerable<RefundSummary>? Refunds { get; set; }

    /// <summary>
    /// List of returns issued against the payment.
    /// </summary>
    [JsonPropertyName("returns")]
    public IEnumerable<BankTransferReturnSummary>? Returns { get; set; }

    /// <summary>
    /// List of re-presented payments linked to the return.
    /// </summary>
    [JsonPropertyName("representment")]
    public PaymentSummary? Representment { get; set; }

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
