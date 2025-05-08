using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

public record GetTransactionSettlementRequest
{
    /// <summary>
    /// Unique identifier of the transaction.
    /// </summary>
    [JsonIgnore]
    public required int TransactionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
