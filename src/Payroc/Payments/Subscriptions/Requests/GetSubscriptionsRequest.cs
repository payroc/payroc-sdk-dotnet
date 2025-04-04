using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Subscriptions;

public record GetSubscriptionsRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier of the subscription that you want to view.
    /// </summary>
    [JsonIgnore]
    public required string SubscriptionId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
