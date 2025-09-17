using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.PaymentLinks.SharingEvents;

[Serializable]
public record ListSharingEventsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the payment link.
    /// </summary>
    [JsonIgnore]
    public required string PaymentLinkId { get; set; }

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
