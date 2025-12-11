using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.PaymentLinks.SharingEvents;

[Serializable]
public record ShareSharingEventsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the payment link.
    /// </summary>
    [JsonIgnore]
    public required string PaymentLinkId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonIgnore]
    public required PaymentLinkEmailShareEvent Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
