using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about U.S. processor fees.
/// </summary>
[Serializable]
public record PricingAgreementUs50Processor : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Polymorphic object that contains fees for card transactions.
    ///
    /// The value of the planType field determines which variant you should use:
    /// -	`interchangePlus` - Interchange + pricing
    /// -	`interchangePlusPlus` - Interchange pricing with three tiers
    /// -	`tiered3` - Three-tiered pricing
    /// -	`tiered4` - Four-tiered pricing
    /// -	`tiered6` - Six-tiered pricing
    /// -	`flatRate` - Flat rate pricing
    /// -	`consumerChoice` - ConsumerChoice
    /// -	`rewardPayChoice` - RewardPayChoice
    /// </summary>
    [JsonPropertyName("card")]
    public PricingAgreementUs50ProcessorCard? Card { get; set; }

    [JsonPropertyName("ach")]
    public Ach? Ach { get; set; }

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
