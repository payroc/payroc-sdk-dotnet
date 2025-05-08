using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.Subscriptions;

public record SubscriptionRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <summary>
    /// Unique identifier that the merchant assigned to the subscription.
    /// </summary>
    [JsonPropertyName("subscriptionId")]
    public required string SubscriptionId { get; set; }

    /// <summary>
    /// Unique identifier that the merchant assigned to the payment plan.
    /// </summary>
    [JsonPropertyName("paymentPlanId")]
    public required string PaymentPlanId { get; set; }

    /// <summary>
    /// Object that contains information about the customer's payment details.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public required SubscriptionRequestPaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Name of the subscription.
    /// This value replaces the name inherited from the payment plan.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Description of the subscription.
    /// This value replaces the description inherited from the payment plan.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("setupOrder")]
    public SubscriptionPaymentOrder? SetupOrder { get; set; }

    [JsonPropertyName("recurringOrder")]
    public SubscriptionRecurringOrder? RecurringOrder { get; set; }

    /// <summary>
    /// Format: **YYYY-MM-DD**
    /// Subscription's start date.
    /// </summary>
    [JsonPropertyName("startDate")]
    public required string StartDate { get; set; }

    /// <summary>
    /// Format: **YYYY-MM-DD**
    /// Subscription's end date.
    /// **Note:** If you provide values for both **length** and **endDate**,
    /// our gateway uses the value for **endDate** to determine when the subscription should end.
    /// </summary>
    [JsonPropertyName("endDate")]
    public string? EndDate { get; set; }

    /// <summary>
    /// Total number of billing cycles. To indicate that the subscription should run indefinitely, send a value of `0`.
    /// This value replaces the **length** inherited from the payment plan.
    /// **Note:** If you provide values for both **length** and **endDate**,
    /// our gateway uses the value for **endDate** to determine when the subscription should end.
    /// </summary>
    [JsonPropertyName("length")]
    public int? Length { get; set; }

    /// <summary>
    /// Number of billing cycles that the merchant wants to pause payments for.
    /// For example, if the merchant wants to offer a free trial period.
    /// </summary>
    [JsonPropertyName("pauseCollectionFor")]
    public int? PauseCollectionFor { get; set; }

    /// <summary>
    /// Array of customField objects.
    /// </summary>
    [JsonPropertyName("customFields")]
    public IEnumerable<CustomField>? CustomFields { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
