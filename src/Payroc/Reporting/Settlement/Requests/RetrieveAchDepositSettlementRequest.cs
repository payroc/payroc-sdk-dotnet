using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record RetrieveAchDepositSettlementRequest
{
    /// <summary>
    /// Unique identifier of the ACH deposit.
    /// </summary>
    [JsonIgnore]
    public required int AchDepositId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
