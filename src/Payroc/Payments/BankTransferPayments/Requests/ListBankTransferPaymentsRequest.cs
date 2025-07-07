using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

[Serializable]
public record ListBankTransferPaymentsRequest
{
    /// <summary>
    /// Filter results by the unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter results by the order ID of the payment.
    /// </summary>
    [JsonIgnore]
    public string? OrderId { get; set; }

    /// <summary>
    /// Filter results by the account holder's name.
    /// </summary>
    [JsonIgnore]
    public string? NameOnAccount { get; set; }

    /// <summary>
    /// Filter results by the last four digits of the account number.
    /// </summary>
    [JsonIgnore]
    public string? Last4 { get; set; }

    /// <summary>
    /// Filter results by transaction type.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListBankTransferPaymentsRequestTypeItem> Type { get; set; } =
        new List<ListBankTransferPaymentsRequestTypeItem>();

    /// <summary>
    /// Filter results by the status of the payment.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListBankTransferPaymentsRequestStatusItem> Status { get; set; } =
        new List<ListBankTransferPaymentsRequestStatusItem>();

    /// <summary>
    /// Filter results by payments that the merchant ran after a specific date. The value follows the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Filter results by payments that the merchant ran before a specific date. The value follows the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Filter results by the settlement status.
    /// </summary>
    [JsonIgnore]
    public ListBankTransferPaymentsRequestSettlementState? SettlementState { get; set; }

    /// <summary>
    /// Filter results by the settlement date. Send a value in **YYYY-MM-DD** format.
    /// </summary>
    [JsonIgnore]
    public DateOnly? SettlementDate { get; set; }

    /// <summary>
    /// Filter results by the paymentLinkId.
    /// </summary>
    [JsonIgnore]
    public string? PaymentLinkId { get; set; }

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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
