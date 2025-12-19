using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that indicates if the merchant accepts American Express Direct cards and contains the merchant’s American Express merchant number.
/// </summary>
[Serializable]
public record ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the merchant accepts American Express Direct.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// If the merchant accepts American Express Direct, provide their American Express merchant number.
    /// </summary>
    [JsonPropertyName("merchantNumber")]
    public string? MerchantNumber { get; set; }

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
