using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about a pricing intent for Merchant Processing Agreement (MPA) 5.0.
/// </summary>
public record PricingIntent50
{
    /// <summary>
    /// Version of the pricing intent.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = "5.0";

    /// <summary>
    /// Unique identifier of the pricing intent.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Date and time that we received your request to create the pricing intent in our system.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Date and time that the pricing intent was last modified.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastUpdatedDate")]
    public DateTime? LastUpdatedDate { get; set; }

    /// <summary>
    /// Status of the pricing intent. The value can be one of the following:
    /// - `active` - We have approved the pricing intent.
    /// - `pendingReview` - We have not yet reviewed the pricing intent.
    /// - `rejected` - We have rejected the pricing intent.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public BaseIntentStatus? Status { get; set; }

    /// <summary>
    /// Unique identifier that you use to connect a merchant to the pricing intent.
    /// </summary>
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    /// <summary>
    /// [Metadata](/api/metadata) object that contains your custom data.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

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
