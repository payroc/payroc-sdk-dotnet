using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record ProcessingTerminalSummary
{
    /// <summary>
    /// Unique identifier of the processing terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Array of links related to your request. For more information about HATEOAS, go to [Hypermedia as the engine of application state.](https://docs.payroc.com/knowledge/basic-concepts/HATEOAS).
    /// </summary>
    [JsonPropertyName("link")]
    public required ProcessingTerminalSummaryLink Link { get; set; }

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
