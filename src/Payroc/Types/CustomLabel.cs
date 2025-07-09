using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the information for the custom label.
/// </summary>
[Serializable]
public record CustomLabel : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Element that you want to provide a custom label for.
    /// </summary>
    [JsonPropertyName("element")]
    public string? Element { get; set; }

    /// <summary>
    /// Custom label to display on the element.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

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
