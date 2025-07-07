using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

[Serializable]
public record RetrieveBankTransferPaymentsRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the payment.
    /// </summary>
    [JsonIgnore]
    public required string PaymentId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
