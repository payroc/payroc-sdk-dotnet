using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record EnhancedInterchange
{
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

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
