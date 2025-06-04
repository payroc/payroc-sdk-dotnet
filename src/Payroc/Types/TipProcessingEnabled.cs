using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Tip settings
/// </summary>
public record TipProcessingEnabled
{
    /// <summary>
    /// Indicates if the terminal prompts for tips.
    /// </summary>
    [JsonPropertyName("tipPrompt")]
    public bool? TipPrompt { get; set; }

    /// <summary>
    /// Indicates if a clerk can adjust a tip after the customer completes the sale.
    /// </summary>
    [JsonPropertyName("tipAdjust")]
    public bool? TipAdjust { get; set; }

    /// <summary>
    /// Object that contains up to three tip amounts that the terminal displays during a sale.
    /// </summary>
    [JsonPropertyName("suggestedTips")]
    public TipProcessingEnabledSuggestedTips? SuggestedTips { get; set; }

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
