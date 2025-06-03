using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.PaymentLinks;

public record DeactivatePaymentLinksRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the payment link.
    /// </summary>
    [JsonIgnore]
    public required string PaymentLinkId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
