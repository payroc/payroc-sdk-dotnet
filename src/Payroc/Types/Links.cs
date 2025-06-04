using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Array of links related to your request. For more information about HATEOAS, go to [HATEOAS.](https://docs.payroc.com/knowledge/basic-concepts/HATEOAS)
/// </summary>
public record Links
{
    [JsonPropertyName("links")]
    public IEnumerable<ProcessingTerminalSummary>? Links_ { get; set; }

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
