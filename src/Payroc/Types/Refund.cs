using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the refund.
/// </summary>
public record Refund
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

    /// <summary>
    /// Operator who requested the refund.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    [JsonPropertyName("order")]
    public required RefundOrder Order { get; set; }

    [JsonPropertyName("customer")]
    public Customer? Customer { get; set; }

    [JsonPropertyName("card")]
    public required Card Card { get; set; }

    [JsonPropertyName("payment")]
    public PaymentSummary? Payment { get; set; }

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
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
