using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about U.S. pricing intents for Merchant Processing Agreement (MPA) 4.0.
/// </summary>
[Serializable]
public record PricingAgreementUs40 : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates the country that the pricing intent applies to.
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; } = "US";

    /// <summary>
    /// Version of the MPA.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = "4.0";

    [JsonPropertyName("base")]
    public required BaseUs Base { get; set; }

    /// <summary>
    /// Object that contains information about U.S. processor fees.
    /// </summary>
    [JsonPropertyName("processor")]
    public PricingAgreementUs40Processor? Processor { get; set; }

    [JsonPropertyName("gateway")]
    public GatewayUs? Gateway { get; set; }

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
