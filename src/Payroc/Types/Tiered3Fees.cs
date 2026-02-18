using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the fees.
/// </summary>
[Serializable]
public record Tiered3Fees : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Object that contains the fees for Mastercard, Visa, and Discover transactions.
    /// </summary>
    [JsonPropertyName("mastercardVisaDiscover")]
    public required QualRates MastercardVisaDiscover { get; set; }

    /// <summary>
    /// Polymorphic object that contains fees for American Express transactions.
    ///
    /// The value of the type field determines which variant you should use:
    /// -	`optBlue` - Amex OptBlue pricing program.
    /// -	`direct` - Amex Direct pricing program.
    /// </summary>
    [JsonPropertyName("amex")]
    public Tiered3FeesAmex? Amex { get; set; }

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
