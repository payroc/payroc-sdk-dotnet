using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

public record GetAuthorizationSettlementRequest
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
