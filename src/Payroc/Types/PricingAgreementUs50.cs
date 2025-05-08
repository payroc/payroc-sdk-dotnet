using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about U.S. pricing intents for Merchant Processing Agreement (MPA) 5.0.
/// </summary>
public record PricingAgreementUs50
{
    /// <summary>
    /// Indicates the country that the pricing intent applies to.
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; } = "US";

    [JsonPropertyName("base")]
    public required BaseUs Base { get; set; }

    /// <summary>
    /// Object that contains information about U.S. processor fees.
    /// </summary>
    [JsonPropertyName("processor")]
    public PricingAgreementUs50Processor? Processor { get; set; }

    [JsonPropertyName("gateway")]
    public GatewayUs50? Gateway { get; set; }

    [JsonPropertyName("services")]
    public IEnumerable<ServiceUs50>? Services { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
