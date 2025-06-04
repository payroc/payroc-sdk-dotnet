using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the shipping preferences for the terminal order.
/// </summary>
public record PropertiesShippingPreferences
{
    /// <summary>
    /// Shipping method for the terminal order. Send one of the following values:
    ///   - 'nextDay'- We schedule the order to be delivered the next day.
    ///   - 'ground' - We ship the order with ground shipping.
    /// </summary>
    [JsonPropertyName("method")]
    public PropertiesShippingPreferencesMethod? Method { get; set; }

    /// <summary>
    /// Indicates if we can schedule the terminal order to be delivered on a Saturday.
    /// </summary>
    [JsonPropertyName("saturdayDelivery")]
    public bool? SaturdayDelivery { get; set; }

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
