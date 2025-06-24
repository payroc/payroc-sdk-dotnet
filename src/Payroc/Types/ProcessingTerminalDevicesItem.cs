using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the details of the device.
/// </summary>
[Serializable]
public record ProcessingTerminalDevicesItem
{
    /// <summary>
    /// Manufacturer of the terminal.
    /// </summary>
    [JsonPropertyName("manufacturer")]
    public required string Manufacturer { get; set; }

    /// <summary>
    /// Model of the terminal.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// Serial number of the terminal.
    /// </summary>
    [JsonPropertyName("serialNumber")]
    public required string SerialNumber { get; set; }

    [JsonPropertyName("communicationType")]
    public CommunicationType? CommunicationType { get; set; }

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
