using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record TaxRate : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates that the tax is a rate.
    /// </summary>
    [JsonPropertyName("type")]
    public required TaxRateType Type { get; set; }

    /// <summary>
    /// Tax percentage for the transaction.
    /// </summary>
    [JsonPropertyName("rate")]
    public required double Rate { get; set; }

    /// <summary>
    /// Name of the tax. A tax validation on the stored rate for the tax name is performed.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

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
