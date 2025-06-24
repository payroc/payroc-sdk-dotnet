using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Refunds;

[Serializable]
public record GetRefundsRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the refund.
    /// </summary>
    [JsonIgnore]
    public required string RefundId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
