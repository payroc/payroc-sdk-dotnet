using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferRefunds;

[Serializable]
public record ListBankTransferRefundsRequest
{
    /// <summary>
    /// Filter results by the unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter results by the order ID of the refund.
    /// </summary>
    [JsonIgnore]
    public string? OrderId { get; set; }

    /// <summary>
    /// Filter results by the accountholder's name.
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
    public IEnumerable<ListBankTransferRefundsRequestTypeItem> Type { get; set; } =
        new List<ListBankTransferRefundsRequestTypeItem>();

    /// <summary>
    /// Filter results by the status of the refund.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<ListBankTransferRefundsRequestStatusItem> Status { get; set; } =
        new List<ListBankTransferRefundsRequestStatusItem>();

    /// <summary>
    /// Filter results by refunds that the merchant ran after a specific date. The value follows the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Filter results by refunds that the merchant ran before a specific date. The value follows the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) standard.
    /// </summary>
    [JsonIgnore]
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Filter results by the settlement status.
    /// </summary>
    [JsonIgnore]
    public ListBankTransferRefundsRequestSettlementState? SettlementState { get; set; }

    /// <summary>
    /// Filter results by the settlement date. Send a value in **YYYY-MM-DD** format.
    /// </summary>
    [JsonIgnore]
    public DateOnly? SettlementDate { get; set; }

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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
