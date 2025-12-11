using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record RetrieveTransactionSettlementRequest
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
