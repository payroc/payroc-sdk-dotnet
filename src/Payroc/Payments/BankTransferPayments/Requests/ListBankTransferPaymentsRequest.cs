using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

[Serializable]
public record ListBankTransferPaymentsRequest
{
    /// <summary>
    /// Filter payments by the unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter payments by the order ID.
    /// </summary>
    [JsonIgnore]
    public string? OrderId { get; set; }

    /// <summary>
    /// Filter payments by the account holder's name.
    /// </summary>
    [JsonIgnore]
    public string? NameOnAccount { get; set; }

    /// <summary>
    /// Filter payments by the last four digits of the account number.
    /// </summary>
    [JsonIgnore]
    public string? Last4 { get; set; }

    /// <summary>
    /// Filter payments by transaction type.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListBankTransferPaymentsRequestTypeItem> Type { get; set; } =
        new List<ListBankTransferPaymentsRequestTypeItem>();

    /// <summary>
    /// Filter by the status of the payment.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListBankTransferPaymentsRequestStatusItem> Status { get; set; } =
        new List<ListBankTransferPaymentsRequestStatusItem>();

    /// <summary>
    /// Filter by payments that the merchant ran after a specific date. The date follows the ISO 8601 standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Filter by payments that the merchant ran before a specific date. The date follows the ISO 8601 standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Filter by the settlement status of the payment.
    /// </summary>
    [JsonIgnore]
    public ListBankTransferPaymentsRequestSettlementState? SettlementState { get; set; }

    /// <summary>
    /// Filter by payments settled on a specific date. The format is in **YYYY-MM-DD**.
    /// </summary>
    [JsonIgnore]
    public DateOnly? SettlementDate { get; set; }

    /// <summary>
    /// Unique identifier that our gateway assigned to the payment link.
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
