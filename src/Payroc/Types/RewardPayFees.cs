using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the fees.
/// </summary>
public record RewardPayFees
{
    /// <summary>
    /// Fee for the monthly subscription. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("monthlySubscription")]
    public required int MonthlySubscription { get; set; }

    /// <summary>
    /// Percentage of the total transaction amount that the processor charges the cardholder.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("cardChargePercentage")]
    public double? CardChargePercentage { get; set; }

    /// <summary>
    /// Percentage of the total transaction amount that the processor charges the merchant.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("merchantChargePercentage")]
    public double? MerchantChargePercentage { get; set; }

    /// <summary>
    /// Fee for each transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("transaction")]
    public int? Transaction { get; set; }

    /// <summary>
    /// Indicates how the merchant manages tips.
    /// </summary>
    [JsonPropertyName("tips")]
    public required RewardPayFeesTips Tips { get; set; }

    [JsonPropertyName("specialityCards")]
    public SpecialityCards? SpecialityCards { get; set; }

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
