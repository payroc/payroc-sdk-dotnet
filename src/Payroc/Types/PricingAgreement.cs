using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record PricingAgreement : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Two-digit code for the country that the pricing intent applies to. The format follows the [ISO-3166-1](https://www.iso.org/iso-3166-country-codes.html) standard.
    /// </summary>
    [JsonPropertyName("country")]
    public PricingAgreementCountry? Country { get; set; }

    /// <summary>
    /// Version of the MPA.
    /// </summary>
    [JsonPropertyName("version")]
    public PricingAgreementVersion? Version { get; set; }

    [JsonPropertyName("base")]
    public BaseUs? Base { get; set; }

    /// <summary>
    /// Object that contains information about U.S. processor fees.
    /// </summary>
    [JsonPropertyName("processor")]
    public PricingAgreementProcessor? Processor { get; set; }

    [JsonPropertyName("gateway")]
    public GatewayUs52? Gateway { get; set; }

    [JsonPropertyName("services")]
    public IEnumerable<ServiceUs50>? Services { get; set; }

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
