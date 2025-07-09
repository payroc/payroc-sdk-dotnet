using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Array of links related to your request. For more information about HATEOAS, go to [Hypermedia as the engine of application state.](https://docs.payroc.com/knowledge/basic-concepts/HATEOAS).
/// </summary>
[Serializable]
public record ProcessingTerminalSummaryLink : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("href")]
    public required string Href { get; set; }

    [JsonPropertyName("rel")]
    public required string Rel { get; set; }

    [JsonPropertyName("method")]
    public required string Method { get; set; }

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
