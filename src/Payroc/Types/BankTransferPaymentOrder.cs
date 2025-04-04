using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the transaction.
/// </summary>
public record BankTransferPaymentOrder
{
    /// <summary>
    /// A unique identifier assigned by the merchant.
    /// </summary>
    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }

    /// <summary>
    /// The processing date and time of the transaction represented as per [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601) standard.
    /// </summary>
    [JsonPropertyName("dateTime")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public DateTime? DateTime { get; set; }

    /// <summary>
    /// A brief description of the transaction.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The total amount in the currency's lowest denomination. For example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    [JsonPropertyName("breakdown")]
    public BankTransferBreakdown? Breakdown { get; set; }

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
