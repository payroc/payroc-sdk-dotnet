using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the surcharge and taxes that apply to the transaction.
/// </summary>
[Serializable]
public record SubscriptionOrderBreakdown
{
    /// <summary>
    /// Total amount for the transaction before tax. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

    /// <summary>
    /// Array of tax objects.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<Tax>? Taxes { get; set; }

    /// <summary>
    /// Object that contains information about the [surcharge](https://docs.payroc.com/knowledge/card-payments/surcharging) that we applied to the transaction.
    /// </summary>
    [JsonPropertyName("surcharge")]
    public Surcharge? Surcharge { get; set; }

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
