using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the initial cost that a customer pays to set up the subscription.
/// </summary>
public record SubscriptionPaymentOrder
{
    /// <summary>
    /// A unique identifier assigned by the merchant.
    /// </summary>
    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }

    /// <summary>
    /// Total amount for the transaction. The value is in the currency's lowest denomination, for example, cents.&lt;br/&gt;
    /// &lt;br/&gt;**Important:** Do not add the surcharge to the amount parameter in the request. If the transaction is eligible for surcharging, our gateway adds the surcharge to the amount in the request, and then returns the updated amount in the response.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>
    /// Description of the transaction.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("breakdown")]
    public SubscriptionOrderBreakdown? Breakdown { get; set; }

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
