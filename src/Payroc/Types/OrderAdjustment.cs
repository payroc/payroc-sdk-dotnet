using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the adjustment to the transaction. Send this object if the merchant is adjusting the order details.
/// </summary>
[Serializable]
public record OrderAdjustment
{
    /// <summary>
    /// Total amount of the transaction.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    [JsonPropertyName("breakdown")]
    public BreakdownAdjustment? Breakdown { get; set; }

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
