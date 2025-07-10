using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

/// <summary>
/// Object that contains the shipping details for the terminal order. If you don't provide a shipping address, we use the Doing Business As (DBA) address of the processing account.
/// </summary>
[Serializable]
public record CreateTerminalOrderShipping : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Object that contains the shipping preferences for the terminal order.
    /// </summary>
    [JsonPropertyName("preferences")]
    public CreateTerminalOrderShippingPreferences? Preferences { get; set; }

    /// <summary>
    /// Object that contains the shipping address for the terminal order.
    /// </summary>
    [JsonPropertyName("address")]
    public CreateTerminalOrderShippingAddress? Address { get; set; }

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
