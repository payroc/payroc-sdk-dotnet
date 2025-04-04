using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about Flat Rate.
/// </summary>
public record FlatRate
{
    /// <summary>
    /// Object that contains information about the Flat Rate fees.
    /// </summary>
    [JsonPropertyName("fees")]
    public required FlatRateFees Fees { get; set; }

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
