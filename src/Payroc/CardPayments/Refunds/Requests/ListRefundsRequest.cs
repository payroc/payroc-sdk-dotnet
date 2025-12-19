using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.CardPayments.Refunds;

[Serializable]
public record ListRefundsRequest
{
    /// <summary>
    /// Filter by terminal ID.
    /// </summary>
    [JsonIgnore]
    public string? ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter refunds by the unique identifier that the merchant assigned to the order.
    /// </summary>
    [JsonIgnore]
    public string? OrderId { get; set; }

    /// <summary>
    /// Filter refunds by the operator who initiated the request.
    /// </summary>
    [JsonIgnore]
    public string? Operator { get; set; }

    /// <summary>
    /// Filter refunds by cardholder name.
    /// </summary>
    [JsonIgnore]
    public string? CardholderName { get; set; }

    /// <summary>
    /// Filter refunds by the first six digits of the card number.
    /// </summary>
    [JsonIgnore]
    public string? First6 { get; set; }

    /// <summary>
    /// Filter refunds by the last four digits of the card number.
    /// </summary>
    [JsonIgnore]
    public string? Last4 { get; set; }

    /// <summary>
    /// Filter by tender type.
    /// </summary>
    [JsonIgnore]
    public ListRefundsRequestTender? Tender { get; set; }

    /// <summary>
    /// Filter refunds by the current status of the refund.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListRefundsRequestStatusItem> Status { get; set; } =
        new List<ListRefundsRequestStatusItem>();

    /// <summary>
    /// Filter by refunds processed after a specific date. The date format follows the ISO 8601 standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Filter by refunds processed before a specific date. The date format follows the ISO 8601 standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Status of the settlement.
    /// </summary>
    [JsonIgnore]
    public ListRefundsRequestSettlementState? SettlementState { get; set; }

    /// <summary>
    /// Date the transaction was settled.
    /// </summary>
    [JsonIgnore]
    public DateOnly? SettlementDate { get; set; }

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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
