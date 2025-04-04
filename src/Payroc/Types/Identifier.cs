using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Identifier
{
    /// <summary>
    /// Type of ID provided to verify identity.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "nationalId";

    /// <summary>
    /// Social Security Number (SSN) or Social Insurance Number (SIN).
    /// </summary>
    [JsonPropertyName("value")]
    public required string Value { get; set; }

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
