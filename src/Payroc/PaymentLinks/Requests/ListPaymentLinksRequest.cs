using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentLinks;

[Serializable]
public record ListPaymentLinksRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter results by the unique identifier that the merchant assigned to the payment link.
    /// </summary>
    [JsonIgnore]
    public string? MerchantReference { get; set; }

    /// <summary>
    /// Filter results by the type of link. Send a value of <c>singleUse</c> or <c>multiUse</c>.
    /// </summary>
    [JsonIgnore]
    public ListPaymentLinksRequestLinkType? LinkType { get; set; }

    /// <summary>
    /// Filter results by the user that entered the amount. Send one of the following values:
    /// - <c>prompt</c> - Customer entered the amount.
    /// - <c>preset</c> - Merchant entered the amount.
    /// </summary>
    [JsonIgnore]
    public ListPaymentLinksRequestChargeType? ChargeType { get; set; }

    /// <summary>
    /// Filter results by the status of the payment link. Send a value of <c>active</c>, <c>completed</c>,<c>deactived</c>, or <c>expired</c>.
    /// </summary>
    [JsonIgnore]
    public ListPaymentLinksRequestStatus? Status { get; set; }

    /// <summary>
    /// Filter results by the customer's name.
    /// </summary>
    [JsonIgnore]
    public string? RecipientName { get; set; }

    /// <summary>
    /// Filter results by the customer's email address.
    /// </summary>
    [JsonIgnore]
    public string? RecipientEmail { get; set; }

    /// <summary>
    /// Filter results by the link's created date. Send a value in **YYYY-MM-DD** format.
    /// </summary>
    [JsonIgnore]
    public DateOnly? CreatedOn { get; set; }

    /// <summary>
    /// Filter results by the link's expiry date. Send a value in **YYYY-MM-DD** format.
    /// </summary>
    [JsonIgnore]
    public DateOnly? ExpiresOn { get; set; }

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
