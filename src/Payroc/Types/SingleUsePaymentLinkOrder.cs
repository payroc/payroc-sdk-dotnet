using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the order.
/// </summary>
public record SingleUsePaymentLinkOrder
{
    /// <summary>
    /// Unique identifier that the merchant assigned to the order.
    /// </summary>
    [JsonPropertyName("orderId")]
    public required string OrderId { get; set; }

    /// <summary>
    /// A brief description of the transaction.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("charge")]
    public required SingleUsePaymentLinkOrderCharge Charge { get; set; }

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
