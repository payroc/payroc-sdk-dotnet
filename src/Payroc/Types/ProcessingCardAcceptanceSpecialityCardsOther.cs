using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record ProcessingCardAcceptanceSpecialityCardsOther : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If the merchant accepts WEX, provide their WEX merchant number.
    /// </summary>
    [JsonPropertyName("wexMerchantNumber")]
    public string? WexMerchantNumber { get; set; }

    /// <summary>
    /// If the merchant accepts Voyager, provide their Voyager merchant ID.
    /// </summary>
    [JsonPropertyName("voyagerMerchantId")]
    public string? VoyagerMerchantId { get; set; }

    /// <summary>
    /// If the merchant accepts Fleet, provide their Fleet merchant ID.
    /// </summary>
    [JsonPropertyName("fleetMerchantId")]
    public string? FleetMerchantId { get; set; }

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
