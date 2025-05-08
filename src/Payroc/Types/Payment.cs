using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Payment
{
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
