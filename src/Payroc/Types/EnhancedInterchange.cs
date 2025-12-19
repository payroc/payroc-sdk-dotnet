using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the fees for enhanced interchange services.
/// </summary>
[Serializable]
public record EnhancedInterchange : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Enrollment fee for the enhanced interchange services. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("enrollment")]
    public required int Enrollment { get; set; }

    /// <summary>
    /// Percentage of additional discount.
    /// </summary>
    [JsonPropertyName("creditToMerchant")]
    public required double CreditToMerchant { get; set; }

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
