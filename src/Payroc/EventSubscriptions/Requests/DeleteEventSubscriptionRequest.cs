using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.EventSubscriptions;

[Serializable]
public record DeleteEventSubscriptionRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the event subscription.
    /// </summary>
    [JsonIgnore]
    public required int SubscriptionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
