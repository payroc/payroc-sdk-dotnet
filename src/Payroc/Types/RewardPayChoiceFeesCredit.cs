using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about fees for credit transactions.
/// </summary>
public record RewardPayChoiceFeesCredit
{
    /// <summary>
    /// Indicates how the merchant manages tips.
    /// </summary>
    [JsonPropertyName("tips")]
    public RewardPayChoiceFeesCreditTips? Tips { get; set; }

    /// <summary>
    /// Percentage of the total transaction amount that the processor charges the cardholder.
    /// </summary>
    [JsonPropertyName("cardChargePercentage")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public double? CardChargePercentage { get; set; }

    /// <summary>
    /// Percentage of the total transaction amount that the processor charges the merchant.
    /// </summary>
    [JsonPropertyName("merchantChargePercentage")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public double? MerchantChargePercentage { get; set; }

    /// <summary>
    /// Fee for each transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("merchantChargePerTransaction")]
    public int? MerchantChargePerTransaction { get; set; }

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
