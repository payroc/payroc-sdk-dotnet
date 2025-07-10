using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Information about the speciality cards that the merchant accepts.
/// </summary>
[Serializable]
public record ProcessingCardAcceptanceSpecialityCards : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("americanExpressDirect")]
    public ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect? AmericanExpressDirect { get; set; }

    [JsonPropertyName("electronicBenefitsTransfer")]
    public ProcessingCardAcceptanceSpecialityCardsElectronicBenefitsTransfer? ElectronicBenefitsTransfer { get; set; }

    [JsonPropertyName("other")]
    public ProcessingCardAcceptanceSpecialityCardsOther? Other { get; set; }

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
