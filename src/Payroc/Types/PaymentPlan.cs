using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record PaymentPlan
{
    /// <summary>
    /// Unique identifier that the merchant assigns to the payment plan.
    /// </summary>
    [JsonPropertyName("paymentPlanId")]
    public required string PaymentPlanId { get; set; }

    /// <summary>
    /// Unique identifier of the terminal that the payment plan is assigned to.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("processingTerminalId")]
    public string? ProcessingTerminalId { get; set; }

    /// <summary>
    /// Name of the payment plan.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Description of the payment plan.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("setupOrder")]
    public PaymentPlanSetupOrder? SetupOrder { get; set; }

    [JsonPropertyName("recurringOrder")]
    public PaymentPlanRecurringOrder? RecurringOrder { get; set; }

    /// <summary>
    /// Object that contains information about the cost of each payment.
    /// To indicate that the payment plan should run indefinitely, send a value of `0`.
    /// </summary>
    [JsonPropertyName("length")]
    public int? Length { get; set; }

    /// <summary>
    /// Indicates how the merchant takes the payment from the customer's account.
    /// - `manual` - The merchant manually collects payments from the customer.
    /// - `automatic` - The terminal automatically collects payments from the customer.
    /// </summary>
    [JsonPropertyName("type")]
    public required PaymentPlanType Type { get; set; }

    /// <summary>
    /// Indicates how often the merchant or the terminal collects a payment from the customer.
    /// </summary>
    [JsonPropertyName("frequency")]
    public required PaymentPlanFrequency Frequency { get; set; }

    /// <summary>
    /// Indicates whether any changes that the merchant makes to the payment plan apply to existing subscriptions.
    /// - `update` - Changes apply to existing subscriptions.
    /// - `continue` - Changes don't apply to existing subscriptions.
    /// </summary>
    [JsonPropertyName("onUpdate")]
    public required PaymentPlanOnUpdate OnUpdate { get; set; }

    /// <summary>
    /// Indicates what happens to existing subscriptions if the merchant deletes the payment plan.
    /// - `complete` - Stops existing subscriptions.
    /// - `continue` - Continues existing subscriptions.
    /// </summary>
    [JsonPropertyName("onDelete")]
    public required PaymentPlanOnDelete OnDelete { get; set; }

    /// <summary>
    /// Array of custom fields that you can use in subscriptions linked to the payment plan.
    /// </summary>
    [JsonPropertyName("customFieldNames")]
    public IEnumerable<string>? CustomFieldNames { get; set; }

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
