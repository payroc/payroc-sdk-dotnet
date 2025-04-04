using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Information about the speciality cards that the merchant accepts.
/// </summary>
public record ProcessingCardAcceptanceSpecialityCards
{
    [JsonPropertyName("americanExpressDirect")]
    public ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect? AmericanExpressDirect { get; set; }

    [JsonPropertyName("electronicBenefitsTransfer")]
    public ProcessingCardAcceptanceSpecialityCardsElectronicBenefitsTransfer? ElectronicBenefitsTransfer { get; set; }

    [JsonPropertyName("other")]
    public ProcessingCardAcceptanceSpecialityCardsOther? Other { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
