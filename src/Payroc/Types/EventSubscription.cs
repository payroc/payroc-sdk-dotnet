using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record EventSubscription
{
    /// <summary>
    /// Unique identifier that we assigned to the event subscription.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("id")]
    public double? Id { get; set; }

    /// <summary>
    /// Indicates if we should notify you if the event occurs. The value is one of the following:
    /// - `true` - We notify you when the event occurs.
    /// - `false` - We don't notify you when the event occurs.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// Status of the subscription. We return one of the following values:
    /// - `registered` - You have set up the subscription, and we will notify you when an event occurs.
    /// - `suspended` - We have deactivated the event subscription, and we won't notify you when an event occurs.
    /// - `failed` - We couldn't contact your URI endpoint. We email the supportEmailAddress.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public EventSubscriptionStatus? Status { get; set; }

    /// <summary>
    /// Array of eventTypes that you want to subscribe to. For a list of all events that you can subscribe to, go to [Events](https://docs.payroc.com/knowledge/basic-concepts/events).
    /// </summary>
    [JsonPropertyName("eventTypes")]
    public IEnumerable<string> EventTypes { get; set; } = new List<string>();

    /// <summary>
    /// Array of notifications, which includes information about how we contact you when an event occurs.
    /// </summary>
    [JsonPropertyName("notifications")]
    public IEnumerable<Notification> Notifications { get; set; } = new List<Notification>();

    /// <summary>
    /// Object that you can send to include custom data in the request. For more information about how to use metadata, go to [Metadata](https://docs.payroc.com/api/metadata).
    /// </summary>
    [JsonPropertyName("metadata")]
    public object? Metadata { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
