using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record ListReportingSettlementTransactionsRequest
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
    /// Filter transactions by the date that the merchant submitted the batch that contains the transaction. The format of this value is **YYYY-MM-DD**.
    ///
    /// You must provide either the batchId or the date.
    /// </summary>
    [JsonIgnore]
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Filter transactions by the unique identifier of the batch that contains the transaction.
    ///
    /// You must provide either the batchId or the date.
    /// </summary>
    [JsonIgnore]
    public int? BatchId { get; set; }

    /// <summary>
    /// Filter results by the unique identifier that the processor assigned to the merchant.
    /// </summary>
    [JsonIgnore]
    public string? MerchantId { get; set; }

    /// <summary>
    /// Filter transactions by transaction type.
    /// </summary>
    [JsonIgnore]
    public ListTransactionsSettlementRequestTransactionType? TransactionType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
