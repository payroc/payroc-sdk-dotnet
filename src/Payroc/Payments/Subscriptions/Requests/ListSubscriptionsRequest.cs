using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Subscriptions;

[Serializable]
public record ListSubscriptionsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter by the customer's name.
    /// </summary>
    [JsonIgnore]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Filter by the last four digits of the card or account number.
    /// </summary>
    [JsonIgnore]
    public string? Last4 { get; set; }

    /// <summary>
    /// Filter by the name of the payment plan.
    /// </summary>
    [JsonIgnore]
    public string? PaymentPlan { get; set; }

    /// <summary>
    /// Filter by the frequency of subscription payments.
    /// </summary>
    [JsonIgnore]
    public ListSubscriptionsRequestFrequency? Frequency { get; set; }

    /// <summary>
    /// Filter by the current status of the subscription.
    /// </summary>
    [JsonIgnore]
    public ListSubscriptionsRequestStatus? Status { get; set; }

    /// <summary>
    /// Format: `YYYY-MM-DD`
    /// Filter subscriptions that end on a specific date.
    /// </summary>
    [JsonIgnore]
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Format: `YYYY-MM-DD`
    /// Filter subscriptions by the date that the next payment is collected.
    /// </summary>
    [JsonIgnore]
    public DateOnly? NextDueDate { get; set; }

    /// <summary>
    /// Points to the resource identifier that you want to receive your results before. Typically, this is the first resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Points to the resource identifier that you want to receive your results after. Typically, this is the last resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// States the total amount of results the response is limited to.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
