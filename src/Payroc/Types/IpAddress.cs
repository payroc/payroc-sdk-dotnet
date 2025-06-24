using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the IP address of the device that sent the request.
/// </summary>
[Serializable]
public record IpAddress
{
    /// <summary>
    /// Internet protocol version of the IP address.
    /// </summary>
    [JsonPropertyName("type")]
    public required IpAddressType Type { get; set; }

    /// <summary>
    /// IP address of the device.
    /// </summary>
    [JsonPropertyName("value")]
    public required string Value { get; set; }

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
