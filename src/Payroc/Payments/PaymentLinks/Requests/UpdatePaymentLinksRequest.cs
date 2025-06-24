using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.PaymentLinks;

[Serializable]
public record UpdatePaymentLinksRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the payment link.
    /// </summary>
    [JsonIgnore]
    public required string PaymentLinkId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonIgnore]
    public IEnumerable<PatchDocument> Body { get; set; } = new List<PatchDocument>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
