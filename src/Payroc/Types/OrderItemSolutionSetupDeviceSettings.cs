using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the device settings if the solution includes a terminal or a peripheral device such as a printer.
/// </summary>
[Serializable]
public record OrderItemSolutionSetupDeviceSettings : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Number of users that we need to set up for mobile solutions.
    /// </summary>
    [JsonPropertyName("numberOfMobileUsers")]
    public int? NumberOfMobileUsers { get; set; }

    /// <summary>
    /// Method of connection between a terminal or a peripheral device and the host.
    /// </summary>
    [JsonPropertyName("communicationType")]
    public OrderItemSolutionSetupDeviceSettingsCommunicationType? CommunicationType { get; set; }

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
