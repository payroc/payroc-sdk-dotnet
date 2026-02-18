using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record EventSubscription : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the event subscription.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    /// <summary>
    /// Indicates if we should notify you when the event occurs. The value is one of the following:
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
    /// Array of events that you want to subscribe to. For a list of events, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).
    /// </summary>
    [JsonPropertyName("eventTypes")]
    public IEnumerable<string> EventTypes { get; set; } = new List<string>();

    /// <summary>
    /// Array of polymorphic notification objects that contain information about how we contact you when an event occurs.
    /// </summary>
    [JsonPropertyName("notifications")]
    public IEnumerable<Notification> Notifications { get; set; } = new List<Notification>();

    /// <summary>
    /// Object that you can send to include custom data in the request. For more information about how to use metadata, go to [Metadata](https://docs.payroc.com/api/metadata).
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object?>? Metadata { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
