using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// A snapshot of the subscription's current state.
/// </summary>
[Serializable]
public record SubscriptionState
{
    /// <summary>
    /// Status of the Subscription.
    ///
    /// - 'active' - Subscription is active.
    /// - 'completed' - Subscription has reached the end date or the total number of billing cycles.
    /// - 'cancelled' - Merchant deactivated the subscription.
    /// - 'suspended' - Subscription is suspended. For example, if the customer misses payments.
    /// </summary>
    [JsonPropertyName("status")]
    public required SubscriptionStateStatus Status { get; set; }

    /// <summary>
    /// Date that the merchant collects the next payment.
    /// </summary>
    [JsonPropertyName("nextDueDate")]
    public DateOnly? NextDueDate { get; set; }

    /// <summary>
    /// Number of payments that the merchant has collected.
    /// </summary>
    [JsonPropertyName("paidInvoices")]
    public required int PaidInvoices { get; set; }

    /// <summary>
    /// Number of payments until the end of the subscription.
    /// Our gateway returns a value for **outstandingInvoices** only if the subscription
    /// has an end date or a fixed number of billing cycles.
    /// </summary>
    [JsonPropertyName("outstandingInvoices")]
    public int? OutstandingInvoices { get; set; }

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
