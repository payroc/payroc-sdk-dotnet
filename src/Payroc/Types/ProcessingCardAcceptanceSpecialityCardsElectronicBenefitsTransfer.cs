using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that indicates if the merchant accepts Electronic Benefits Transfer (EBT) cards and contains the merchant’s Food and Nutrition Services (FNS) number.
/// </summary>
[Serializable]
public record ProcessingCardAcceptanceSpecialityCardsElectronicBenefitsTransfer
    : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the merchant accepts EBT.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// If the merchant accepts EBT, provide their FNS number.
    /// </summary>
    [JsonPropertyName("fnsNumber")]
    public string? FnsNumber { get; set; }

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
