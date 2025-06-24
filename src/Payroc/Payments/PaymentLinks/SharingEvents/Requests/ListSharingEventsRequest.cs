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
