using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[Serializable]
public record RetrieveAuthorizationSettlementRequest
{
    /// <summary>
    /// Unique identifier of the authorization.
    /// </summary>
    [JsonIgnore]
    public required int AuthorizationId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
