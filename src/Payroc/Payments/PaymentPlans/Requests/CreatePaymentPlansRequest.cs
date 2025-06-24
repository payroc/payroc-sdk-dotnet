using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.PaymentPlans;

[Serializable]
public record CreatePaymentPlansRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonIgnore]
    public required PaymentPlan Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
