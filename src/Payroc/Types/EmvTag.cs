using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the EMV tag.
/// </summary>
public record EmvTag
{
    /// <summary>
    /// Hex code of the EMV tag.
    /// </summary>
    [JsonPropertyName("hex")]
    public required string Hex { get; set; }

    /// <summary>
    /// Value of the EMV tag.
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
