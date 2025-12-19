using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding;

/// <summary>
/// Object that contains funding information for the processing account, including funding schedules, funding fees, and details of funding accounts.
/// </summary>
[Serializable]
public record Funding : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Object that contains funding accounts associated with the processing account.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("fundingAccounts")]
    public IEnumerable<FundingAccountSummary>? FundingAccounts { get; set; }

    /// <summary>
    /// Indicates if the processing account can receive funds.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public CommonFundingStatus? Status { get; set; }

    /// <summary>
    /// Indicates when funds are sent to the funding account.
    ///
    /// If you send a value of `sameDay` or `nextDay`, provide a value for acceleratedFundingFee.
    ///
    /// **Note:** If you send a value of `sameday`, funding includes all transactions the merchant ran before the ACH cut-off time.
    /// </summary>
    [JsonPropertyName("fundingSchedule")]
    public CommonFundingFundingSchedule? FundingSchedule { get; set; }

    /// <summary>
    /// Monthly fee in cents for accelerated funding. The value is in the currency's lowest denomination, for example, cents.
    ///
    /// We apply this fee if the value for fundingSchedule is `sameday` or `nextday`.
    /// </summary>
    [JsonPropertyName("acceleratedFundingFee")]
    public int? AcceleratedFundingFee { get; set; }

    /// <summary>
    /// Indicates if we collect fees from the merchant's account each day.
    /// </summary>
    [JsonPropertyName("dailyDiscount")]
    public bool? DailyDiscount { get; set; }

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
