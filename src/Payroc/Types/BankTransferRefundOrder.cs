using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the order.
/// </summary>
public record BankTransferRefundOrder
{
    /// <summary>
    /// Unique identifier that the merchant assigned to the transaction.
    /// </summary>
    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Date and time that we processed the transaction. We return this value in the ISO 8601 format.
    /// </summary>
    [JsonPropertyName("dateTime")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Description of the transaction.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Total amount of the transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

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
