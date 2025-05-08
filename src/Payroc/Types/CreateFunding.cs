using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record CreateFunding
{
    /// <summary>
    /// Array of fundingAccounts objects.
    /// </summary>
    [JsonAccess(JsonAccessType.WriteOnly)]
    [JsonPropertyName("fundingAccounts")]
    public IEnumerable<FundingAccount>? FundingAccounts { get; set; }

    /// <summary>
    /// Indicates if the processing account can receive funds.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
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
