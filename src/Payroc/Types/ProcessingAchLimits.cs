using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record ProcessingAchLimits
{
    /// <summary>
    /// Maximum amount allowed for a single debit or credit transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("singleTransaction")]
    public required int SingleTransaction { get; set; }

    /// <summary>
    /// Maximum amount of total transactions allowed per day. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("dailyDeposit")]
    public required int DailyDeposit { get; set; }

    /// <summary>
    /// Maximum amount of total transactions allowed per month. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("monthlyDeposit")]
    public required int MonthlyDeposit { get; set; }

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
