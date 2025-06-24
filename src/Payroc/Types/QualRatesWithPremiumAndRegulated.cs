using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record QualRatesWithPremiumAndRegulated
{
    [JsonPropertyName("regulatedCheckCard")]
    public required ProcessorFee RegulatedCheckCard { get; set; }

    [JsonPropertyName("unregulatedCheckCard")]
    public required ProcessorFee UnregulatedCheckCard { get; set; }

    [JsonPropertyName("premiumRate")]
    public required ProcessorFee PremiumRate { get; set; }

    [JsonPropertyName("qualifiedRate")]
    public required ProcessorFee QualifiedRate { get; set; }

    [JsonPropertyName("midQualRate")]
    public required ProcessorFee MidQualRate { get; set; }

    [JsonPropertyName("nonQualRate")]
    public required ProcessorFee NonQualRate { get; set; }

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
