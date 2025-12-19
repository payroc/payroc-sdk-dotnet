using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingActivity;

[Serializable]
public record ListFundingActivityRequest
{
    /// <summary>
    /// Return the previous page of results before the value that you specify.
    ///
    /// You can’t send the before parameter in the same request as the after parameter.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Return the next page of results after the value that you specify.
    ///
    /// You can’t send the after parameter in the same request as the before parameter.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// Limit the maximum number of results that we return for each page.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Filter by activity after a specific date. Send a value in **YYYY-MM-DD** format.
    /// </summary>
    [JsonIgnore]
    public required DateOnly DateFrom { get; set; }

    /// <summary>
    /// Filter by activity before a specific date. Send a value in **YYYY-MM-DD** format.
    /// </summary>
    [JsonIgnore]
    public required DateOnly DateTo { get; set; }

    /// <summary>
    /// Filter results by the unique identifier that the processor assigned to the merchant.
    /// </summary>
    [JsonIgnore]
    public string? MerchantId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
