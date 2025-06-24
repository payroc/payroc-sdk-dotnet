using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the payment.
/// </summary>
[Serializable]
public record PaymentOrder
{
    /// <summary>
    /// A unique identifier assigned by the merchant.
    /// </summary>
    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Date and time that the processor processed the transaction. Our gateway returns this value in the ISO 8601 format.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Description of the transaction.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Total amount of the transaction. The value is in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    [JsonPropertyName("breakdown")]
    public ItemizedBreakdown? Breakdown { get; set; }

    [JsonPropertyName("dccOffer")]
    public DccOffer? DccOffer { get; set; }

    [JsonPropertyName("standingInstructions")]
    public StandingInstructions? StandingInstructions { get; set; }

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
