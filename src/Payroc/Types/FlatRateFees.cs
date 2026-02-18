using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Flat Rate fees.
/// </summary>
[Serializable]
public record FlatRateFees : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Object that contains the fees for standard card transactions.
    /// </summary>
    [JsonPropertyName("standardCards")]
    public required ProcessorFee StandardCards { get; set; }

    /// <summary>
    /// Polymorphic object that contains fees for American Express transactions.
    /// </summary>
    [JsonPropertyName("amex")]
    public FlatRateFeesAmex? Amex { get; set; }

    [JsonPropertyName("pinDebit")]
    public PinDebit? PinDebit { get; set; }

    [JsonPropertyName("electronicBenefitsTransfer")]
    public ElectronicBenefitsTransfer? ElectronicBenefitsTransfer { get; set; }

    [JsonPropertyName("specialityCards")]
    public SpecialityCards? SpecialityCards { get; set; }

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
