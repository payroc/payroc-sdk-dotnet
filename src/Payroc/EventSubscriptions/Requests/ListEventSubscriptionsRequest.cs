using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.EventSubscriptions;

public record ListEventSubscriptionsRequest
{
    /// <summary>
    /// Filter event subscriptions by subscription status.
    /// </summary>
    [JsonIgnore]
    public ListEventSubscriptionsRequestStatus? Status { get; set; }

    /// <summary>
    /// Filter event subscriptions by event type. For a complete list of events, go to [Events](#https://docs.payroc.com/knowledge/basic-concepts/events).
    /// </summary>
    [JsonIgnore]
    public string? Event { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
