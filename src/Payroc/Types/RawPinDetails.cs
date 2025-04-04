using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the unencrypted PIN details.
/// </summary>
public record RawPinDetails
{
    /// <summary>
    /// Customer’s unencrypted PIN.
    /// </summary>
    [JsonPropertyName("pin")]
    public required string Pin { get; set; }

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
