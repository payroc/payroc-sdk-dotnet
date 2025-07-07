using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record SignatureByDirectLinkLink
{
    /// <summary>
    /// Indicates the relationship between the current resource and the target resource.
    /// </summary>
    [JsonPropertyName("rel")]
    public required string Rel { get; set; }

    /// <summary>
    /// HTTP method that you need to use with the target resource.
    /// </summary>
    [JsonPropertyName("method")]
    public required string Method { get; set; }

    /// <summary>
    /// URL of the target resource.
    /// </summary>
    [JsonPropertyName("href")]
    public required string Href { get; set; }

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
