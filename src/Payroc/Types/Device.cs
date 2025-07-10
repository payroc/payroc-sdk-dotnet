using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the physical device the merchant used to capture the customer’s card details.
/// </summary>
[Serializable]
public record Device : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Model of the device that the merchant used to process the transaction.
    /// </summary>
    [JsonPropertyName("model")]
    public required DeviceModel Model { get; set; }

    /// <summary>
    /// Indicates if the device is attended or unattended.
    /// </summary>
    [JsonPropertyName("category")]
    public DeviceCategory? Category { get; set; }

    /// <summary>
    /// Serial number of the physical device.
    /// </summary>
    [JsonPropertyName("serialNumber")]
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Firmware version of the physical device.
    /// </summary>
    [JsonPropertyName("firmwareVersion")]
    public string? FirmwareVersion { get; set; }

    [JsonPropertyName("config")]
    public DeviceConfig? Config { get; set; }

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
