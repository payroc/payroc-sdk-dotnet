using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.PaymentPlans;

public record DeletePaymentPlansRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that the merchant assigned to the payment plan.
    /// </summary>
    [JsonIgnore]
    public required string PaymentPlanId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
