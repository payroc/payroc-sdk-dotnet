using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Notifications.EventSubscriptions;

[Serializable]
public record DeleteEventSubscriptionsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the event subscription.
    /// **Note:** Our gateway returned the subscriptionId in the id field in the response of the [Create Event Subscription](https://docs.payroc.com/api/schema/notifications/event-subscriptions/create) method.
    /// </summary>
    [JsonIgnore]
    public required int SubscriptionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
