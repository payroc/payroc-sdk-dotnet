using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Subscriptions;

public record ReactivateSubscriptionsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that the merchant assigned to the subscription.
    /// </summary>
    [JsonIgnore]
    public required string SubscriptionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
