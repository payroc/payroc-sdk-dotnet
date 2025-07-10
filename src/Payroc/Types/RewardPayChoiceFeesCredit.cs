using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about fees for credit transactions.
/// </summary>
[Serializable]
public record RewardPayChoiceFeesCredit : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates how the merchant manages tips.
    /// </summary>
    [JsonPropertyName("tips")]
    public RewardPayChoiceFeesCreditTips? Tips { get; set; }

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
    [JsonPropertyName("merchantChargePerTransaction")]
    public int? MerchantChargePerTransaction { get; set; }

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
