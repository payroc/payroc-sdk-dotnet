using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Notifications.EventSubscriptions;

[Serializable]
public record ListEventSubscriptionsRequest
{
    /// <summary>
    /// Filter event subscriptions by subscription status.
    /// </summary>
    [JsonIgnore]
    public ListEventSubscriptionsRequestStatus? Status { get; set; }

    /// <summary>
    /// Filter event subscriptions by an event type. For a list of event types, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).
    /// </summary>
    [JsonIgnore]
    public string? Event { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
