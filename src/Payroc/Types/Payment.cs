using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record Payment : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that our gateway assigned to the transaction.
    /// </summary>
    [JsonPropertyName("paymentId")]
    public required string PaymentId { get; set; }

    /// <summary>
    /// Unique identifier of the terminal that initiated the transaction.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Operator who initiated the request.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("order")]
    public required PaymentOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("card")]
    public required Card Card { get; set; }

    /// <summary>
    /// Array of refundSummary objects.
    /// Each object contains information about refunds linked to the transaction.
    /// </summary>
    [JsonPropertyName("refunds")]
    public IEnumerable<RefundSummary>? Refunds { get; set; }

    [JsonPropertyName("supportedOperations")]
    public IEnumerable<SupportedOperationsItem>? SupportedOperations { get; set; }

    [JsonPropertyName("transactionResult")]
    public required TransactionResult TransactionResult { get; set; }

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
