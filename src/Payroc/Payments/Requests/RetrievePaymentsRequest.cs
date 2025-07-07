using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[Serializable]
public record RetrievePaymentsRequest
{
    /// <summary>
    /// Unique identifier of the payment that the merchant wants to retrieve.
    /// </summary>
    [JsonIgnore]
    public required string PaymentId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
