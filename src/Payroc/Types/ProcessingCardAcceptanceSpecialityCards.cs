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

    /// <summary>
    /// Object that indicates if the merchant accepts American Express Direct cards and contains the merchant’s American Express merchant number.
    /// </summary>
    [JsonPropertyName("americanExpressDirect")]
    public ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect? AmericanExpressDirect { get; set; }

    /// <summary>
    /// Object that indicates if the merchant accepts Electronic Benefits Transfer (EBT) cards and contains the merchant’s Food and Nutrition Services (FNS) number.
    /// </summary>
    [JsonPropertyName("electronicBenefitsTransfer")]
    public ProcessingCardAcceptanceSpecialityCardsElectronicBenefitsTransfer? ElectronicBenefitsTransfer { get; set; }

    /// <summary>
    /// Object that contains information about other speciality cards that the merchant accepts.
    /// </summary>
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
