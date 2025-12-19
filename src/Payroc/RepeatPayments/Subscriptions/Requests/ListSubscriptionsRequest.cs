using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.RepeatPayments.Subscriptions;

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
    /// Return the previous page of results before the value that you specify.
    ///
    /// You can’t send the before parameter in the same request as the after parameter.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Return the next page of results after the value that you specify.
    ///
    /// You can’t send the after parameter in the same request as the before parameter.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// Limit the maximum number of results that we return for each page.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
