using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record QualRates : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Object that contains the fees for a qualified transaction.
    /// </summary>
    [JsonPropertyName("qualifiedRate")]
    public required ProcessorFee QualifiedRate { get; set; }

    /// <summary>
    /// Object that contains the fees for a mid-qualified transaction.
    /// </summary>
    [JsonPropertyName("midQualRate")]
    public required ProcessorFee MidQualRate { get; set; }

    /// <summary>
    /// Object that contains the fees for a non-qualified transaction.
    /// </summary>
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
