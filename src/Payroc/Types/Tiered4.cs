using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about tiered pricing with four tiers.
/// </summary>
public record Tiered4
{
    /// <summary>
    /// Object that contains information about the fees.
    /// </summary>
    [JsonPropertyName("fees")]
    public required Tiered4Fees Fees { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
