using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Tip settings
/// </summary>
[Serializable]
public record TipProcessingEnabled : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the terminal can accept tips.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
