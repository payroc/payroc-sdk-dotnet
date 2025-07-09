using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record QualRatesWithPremium : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("premiumRate")]
    public required ProcessorFee PremiumRate { get; set; }

    [JsonPropertyName("qualifiedRate")]
    public required ProcessorFee QualifiedRate { get; set; }

    [JsonPropertyName("midQualRate")]
    public required ProcessorFee MidQualRate { get; set; }

    [JsonPropertyName("nonQualRate")]
    public required ProcessorFee NonQualRate { get; set; }

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
