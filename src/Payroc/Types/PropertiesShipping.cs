using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the shipping details for the terminal order. If you don't provide a shipping address, we use the Doing Business As (DBA) address of the processing account.
/// </summary>
public record PropertiesShipping
{
    /// <summary>
    /// Object that contains the shipping preferences for the terminal order.
    /// </summary>
    [JsonPropertyName("preferences")]
    public PropertiesShippingPreferences? Preferences { get; set; }

    /// <summary>
    /// Object that contains the shipping address for the terminal order.
    /// </summary>
    [JsonPropertyName("address")]
    public PropertiesShippingAddress? Address { get; set; }

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
