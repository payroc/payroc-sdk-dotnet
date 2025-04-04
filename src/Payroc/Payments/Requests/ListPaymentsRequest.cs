using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

public record ListPaymentsRequest
{
    /// <summary>
    /// Filter payments by terminal ID.
    /// </summary>
    [JsonIgnore]
    public string? ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter payments by order ID.
    /// </summary>
    [JsonIgnore]
    public string? OrderId { get; set; }

    /// <summary>
    /// Filter payments by operator.
    /// </summary>
    [JsonIgnore]
    public string? Operator { get; set; }

    /// <summary>
    /// Filter payments by the cardholder’s name.
    /// </summary>
    [JsonIgnore]
    public string? CardholderName { get; set; }

    /// <summary>
    /// Filter payments by the first six digits of the card number that the customer used in the transaction.
    /// </summary>
    [JsonIgnore]
    public string? First6 { get; set; }

    /// <summary>
    /// Filter payments by the last four digits of the card number that the customer used in the transaction.
    /// </summary>
    [JsonIgnore]
    public string? Last4 { get; set; }

    /// <summary>
    /// Filter by tender type.
    /// </summary>
    [JsonIgnore]
    public ListPaymentsRequestTender? Tender { get; set; }

    /// <summary>
    /// Filter payments by tip.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListPaymentsRequestTipModeItem> TipMode { get; set; } =
        new List<ListPaymentsRequestTipModeItem>();

    /// <summary>
    /// Filter payments by transaction type.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListPaymentsRequestTypeItem> Type { get; set; } =
        new List<ListPaymentsRequestTypeItem>();

    /// <summary>
    /// Filter payments by the status of the transaction.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListPaymentsRequestStatusItem> Status { get; set; } =
        new List<ListPaymentsRequestStatusItem>();

    /// <summary>
    /// Filter by payments that the processor processed after a specific date. The date format follows the ISO 8601 standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Filter by payments that the processer processed before a specific date. The date format follows the ISO 8601 standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Filter payments by the settlement status of the transaction.
    /// </summary>
    [JsonIgnore]
    public ListPaymentsRequestSettlementState? SettlementState { get; set; }

    /// <summary>
    /// Filter by payments that the processor settled on a specific date in the format **YYYY-MM-DD**.
    /// </summary>
    [JsonIgnore]
    public string? SettlementDate { get; set; }

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
