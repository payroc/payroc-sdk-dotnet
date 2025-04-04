using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding;

/// <summary>
/// Object that contains funding information.
/// </summary>
public record Funding
{
    [JsonPropertyName("fundingAccounts")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public IEnumerable<FundingAccountSummary>? FundingAccounts { get; set; }

    /// <summary>
    /// Indicates if the processing account can receive funds.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public CommonFundingStatus? Status { get; set; }

    /// <summary>
    /// Indicates when funds are sent to the funding account.
    /// **Note:** If you send a value of `sameday`, funding includes all transactions the merchant ran before the ACH cut-off time.
    /// </summary>
    [JsonPropertyName("fundingSchedule")]
    public CommonFundingFundingSchedule? FundingSchedule { get; set; }

    /// <summary>
    /// Monthly fee in cents for accelerated funding. We apply this fee if the value for fundingSchedule is `sameday` or `nextday`.
    /// </summary>
    [JsonPropertyName("acceleratedFundingFee")]
    public int? AcceleratedFundingFee { get; set; }

    /// <summary>
    /// Indicator if fees should be taken on a daily basis.
    /// </summary>
    [JsonPropertyName("dailyDiscount")]
    public bool? DailyDiscount { get; set; }

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
