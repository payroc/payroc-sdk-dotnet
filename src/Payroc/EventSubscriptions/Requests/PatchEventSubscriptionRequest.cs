using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.EventSubscriptions;

public record PatchEventSubscriptionRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the event subscription.
    /// </summary>
    [JsonIgnore]
    public required int SubscriptionId { get; set; }

    [JsonIgnore]
    public IEnumerable<PatchDocument> Body { get; set; } = new List<PatchDocument>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
