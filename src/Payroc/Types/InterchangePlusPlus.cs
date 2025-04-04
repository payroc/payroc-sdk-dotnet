using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about Interchange Plus Plus.
/// </summary>
public record InterchangePlusPlus
{
    /// <summary>
    /// Object that contains information about the fees.
    /// </summary>
    [JsonPropertyName("fees")]
    public required InterchangePlusPlusFees Fees { get; set; }

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
