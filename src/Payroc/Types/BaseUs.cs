using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about U.S. base fees.
/// </summary>
[Serializable]
public record BaseUs : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Fee for each address verification request. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("addressVerification")]
    public int? AddressVerification { get; set; }

    /// <summary>
    /// Object that contains information about the annual fee.
    /// </summary>
    [JsonPropertyName("annualFee")]
    public required BaseUsAnnualFee AnnualFee { get; set; }

    /// <summary>
    /// Annual fee for the regulatory assistance program. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("regulatoryAssistanceProgram")]
    public int? RegulatoryAssistanceProgram { get; set; }

    /// <summary>
    /// Fee that we apply each month if you aren't compliant with PCI standards. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("pciNonCompliance")]
    public int? PciNonCompliance { get; set; }

    /// <summary>
    /// Monthly fee for Payroc Advantage. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("merchantAdvantage")]
    public int? MerchantAdvantage { get; set; }

    /// <summary>
    /// Object that contains information about the Platinum Security fee.
    /// </summary>
    [JsonPropertyName("platinumSecurity")]
    public BaseUsPlatinumSecurity? PlatinumSecurity { get; set; }

    /// <summary>
    /// Monthly fee for maintenance. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("maintenance")]
    public required int Maintenance { get; set; }

    /// <summary>
    /// Monthly fee that we charge when the merchant doesn't meet the minimum fee amount. This monthly fee is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("minimum")]
    public required int Minimum { get; set; }

    /// <summary>
    /// Fee for each voice authorization. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("voiceAuthorization")]
    public int? VoiceAuthorization { get; set; }

    /// <summary>
    /// Fee for each chargeback. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("chargeback")]
    public int? Chargeback { get; set; }

    /// <summary>
    /// Fee for each retrieval. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("retrieval")]
    public int? Retrieval { get; set; }

    /// <summary>
    /// Fee for each batch. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("batch")]
    public required int Batch { get; set; }

    /// <summary>
    /// Fee for early termination. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("earlyTermination")]
    public int? EarlyTermination { get; set; }

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
