using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the 3-D Secure information from our gateway.
/// </summary>
[Serializable]
public record GatewayThreeDSecure
{
    /// <summary>
    /// Reference that our gateway assigned to the 3-D Secure authentication response.
    /// </summary>
    [JsonPropertyName("mpiReference")]
    public required string MpiReference { get; set; }

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
