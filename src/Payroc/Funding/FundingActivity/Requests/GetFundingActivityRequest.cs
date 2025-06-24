using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingActivity;

[Serializable]
public record GetFundingActivityRequest
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
    /// Retrieve activity that occured since `dateFrom`. We can return activity from only the last two years.
    /// </summary>
    [JsonIgnore]
    public required DateOnly DateFrom { get; set; }

    /// <summary>
    /// Retrieve activity that occured before `dateTo`.
    /// </summary>
    [JsonIgnore]
    public required DateOnly DateTo { get; set; }

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
