using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.PaymentLinks;

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
    /// Filter results by the type of link. Send a value of &lt;code&gt;singleUse&lt;/code&gt; or &lt;code&gt;multiUse&lt;/code&gt;.
    /// </summary>
    [JsonIgnore]
    public ListPaymentLinksRequestLinkType? LinkType { get; set; }

    /// <summary>
    /// Filter results by the user that entered the amount. Send one of the following values:
    /// - &lt;code&gt;prompt&lt;/code&gt; - Customer entered the amount.
    /// - &lt;code&gt;preset&lt;/code&gt; - Merchant entered the amount.
    /// </summary>
    [JsonIgnore]
    public ListPaymentLinksRequestChargeType? ChargeType { get; set; }

    /// <summary>
    /// Filter results by the status of the payment link. Send a value of &lt;code&gt;active&lt;/code&gt;, &lt;code&gt;completed&lt;/code&gt;,&lt;code&gt;deactived&lt;/code&gt;, or &lt;code&gt;expired&lt;/code&gt;.
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
