using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains up to three tip amounts that the terminal displays during a sale.
/// </summary>
[Serializable]
public record TipProcessingEnabledSuggestedTips
{
    /// <summary>
    /// Indicates if the terminal displays tip amounts during a sale.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// Array of the tip amounts that the terminal displays during a sale.
    /// </summary>
    [JsonPropertyName("tipPercentages")]
    public IEnumerable<string>? TipPercentages { get; set; }

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
