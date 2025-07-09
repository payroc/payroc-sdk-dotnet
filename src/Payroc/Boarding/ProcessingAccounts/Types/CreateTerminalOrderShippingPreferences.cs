using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

/// <summary>
/// Object that contains the shipping preferences for the terminal order.
/// </summary>
[Serializable]
public record CreateTerminalOrderShippingPreferences : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Shipping method for the terminal order. Send one of the following values:
    ///   - 'nextDay'- We schedule the order to be delivered the next day.
    ///   - 'ground' - We ship the order with ground shipping.
    /// </summary>
    [JsonPropertyName("method")]
    public CreateTerminalOrderShippingPreferencesMethod? Method { get; set; }

    /// <summary>
    /// Indicates if we can schedule the terminal order to be delivered on a Saturday.
    /// </summary>
    [JsonPropertyName("saturdayDelivery")]
    public bool? SaturdayDelivery { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
