using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the device settings if the solution includes a terminal or a peripheral device such as a printer.
/// </summary>
[Serializable]
public record OrderItemSolutionSetupDeviceSettings
{
    /// <summary>
    /// Number of users that we need to set up for mobile solutions.
    /// </summary>
    [JsonPropertyName("numberOfMobileUsers")]
    public float? NumberOfMobileUsers { get; set; }

    /// <summary>
    /// Method of connection between a terminal or a peripheral device and the host.
    /// </summary>
    [JsonPropertyName("communicationType")]
    public OrderItemSolutionSetupDeviceSettingsCommunicationType? CommunicationType { get; set; }

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
