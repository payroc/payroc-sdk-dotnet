using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

public record ListReportingSettlementTransactionsRequest
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
    /// Date to retrieve results from. The format of this value is **YYYY-MM-DD**. You must provide either the 'batchId' or the 'date'.
    /// </summary>
    [JsonIgnore]
    public required DateOnly Date { get; set; }

    /// <summary>
    /// Unique identifier of the batch. You must provide either the 'batchId' or the 'date'.
    /// </summary>
    [JsonIgnore]
    public required int BatchId { get; set; }

    /// <summary>
    /// Unique identifier of the merchant.
    /// </summary>
    [JsonIgnore]
    public string? MerchantId { get; set; }

    /// <summary>
    /// Type of transaction.
    /// </summary>
    [JsonIgnore]
    public ListTransactionsSettlementRequestTransactionType? TransactionType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
