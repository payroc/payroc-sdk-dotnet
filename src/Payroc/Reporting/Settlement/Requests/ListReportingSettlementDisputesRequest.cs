using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record ListReportingSettlementDisputesRequest
{
    /// <summary>
    /// Return the previous page of results before the value that you specify.
    ///
    /// You can’t send a before parameter in the same request as an after parameter.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Return the next page of results after the value that you specify.
    ///
    /// You can’t send an after parameter in the same request as a before parameter.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// Limit the maximum number of results that we return for each page.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Filter results by the date that the dispute was submitted.
    /// </summary>
    [JsonIgnore]
    public required DateOnly Date { get; set; }

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
