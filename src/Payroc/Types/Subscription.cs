using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record Subscription : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that the merchant assigned to the subscription.
    /// </summary>
    [JsonPropertyName("subscriptionId")]
    public required string SubscriptionId { get; set; }

    /// <summary>
    /// Unique identifier of the terminal that the subscription is linked to.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("paymentPlan")]
    public required PaymentPlanSummary PaymentPlan { get; set; }

    [JsonPropertyName("secureToken")]
    public required SecureTokenSummary SecureToken { get; set; }

    /// <summary>
    /// Name of the subscription.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Description of the subscription.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("setupOrder")]
    public SubscriptionPaymentOrder? SetupOrder { get; set; }

    [JsonPropertyName("recurringOrder")]
    public SubscriptionRecurringOrder? RecurringOrder { get; set; }

    [JsonPropertyName("currentState")]
    public required SubscriptionState CurrentState { get; set; }

    /// <summary>
    /// Format: **YYYY-MM-DD**
    /// Subscription's start date.
    /// </summary>
    [JsonPropertyName("startDate")]
    public required DateOnly StartDate { get; set; }

    /// <summary>
    /// Format: **YYYY-MM-DD**
    /// Subscription's end date.
    /// **Note:** If you provide values for both **length** and **endDate**,
    /// our gateway uses the value for **endDate** to determine when the subscription should end.
    /// </summary>
    [JsonPropertyName("endDate")]
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Total number of billing cycles. To indicate that the subscription should run indefinitely, send a value of `0`. This value replaces the **length** inherited from the payment plan.
    /// **Note:** If you provide values for both **length** and **endDate**, our gateway uses the value for **endDate** to determine when the subscription should end.
    /// </summary>
    [JsonPropertyName("length")]
    public int? Length { get; set; }

    /// <summary>
    /// How the merchant takes the payment from the customer’s account.
    /// - `manual` – The merchant manually collects payments from the customer.
    /// - `automatic` – The terminal automatically collects payments from the customer.
    /// </summary>
    [JsonPropertyName("type")]
    public required SubscriptionType Type { get; set; }

    /// <summary>
    /// Indicates how often the merchant or the terminal collects a payment from the customer.
    /// </summary>
    [JsonPropertyName("frequency")]
    public required SubscriptionFrequency Frequency { get; set; }

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
