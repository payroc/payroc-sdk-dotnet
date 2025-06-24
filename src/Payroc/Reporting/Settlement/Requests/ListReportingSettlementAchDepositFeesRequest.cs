using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record ListReportingSettlementAchDepositFeesRequest
{
    /// <summary>
    /// Points to the resource identifier that you want to receive your results before. Typically, this is the first resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Points to the resource identifier that you want to receive your results after. Typically, this is the last resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// States the total amount of results the response is limited to.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Date to retrieve results from. You must provide either the 'achDepositId' or the 'date'.
    /// </summary>
    [JsonIgnore]
    public required DateOnly Date { get; set; }

    /// <summary>
    /// Unique identifier of the ACH deposit. You must provide either the 'achDepositId' or the 'date'.
    /// </summary>
    [JsonIgnore]
    public required int AchDepositId { get; set; }

    /// <summary>
    /// Unique identifier of the merchant.
    /// </summary>
    [JsonIgnore]
    public string? MerchantId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
