using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Notifications.EventSubscriptions;

[Serializable]
public record PartiallyUpdateEventSubscriptionsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the event subscription.
    /// **Note:** Our gateway returned the subscriptionId in the id field in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.
    /// </summary>
    [JsonIgnore]
    public required int SubscriptionId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
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
