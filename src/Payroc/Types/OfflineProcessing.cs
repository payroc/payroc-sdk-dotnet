using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the transaction if the merchant ran it when the terminal was offline.
/// </summary>
public record OfflineProcessing
{
    /// <summary>
    /// Status of the transaction.
    /// </summary>
    [JsonPropertyName("operation")]
    public required OfflineProcessingOperation Operation { get; set; }

    /// <summary>
    /// Approval code for the transaction from the processor.
    /// </summary>
    [JsonPropertyName("approvalCode")]
    public string? ApprovalCode { get; set; }

    /// <summary>
    /// Date and time that the merchant ran the transaction. The date follows the ISO 8601 standard.
    /// </summary>
    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; set; }

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
