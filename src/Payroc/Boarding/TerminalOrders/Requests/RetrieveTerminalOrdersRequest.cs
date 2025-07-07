using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.TerminalOrders;

[Serializable]
public record RetrieveTerminalOrdersRequest
{
    /// <summary>
    /// Unique identifier of the terminal order.
    /// </summary>
    [JsonIgnore]
    public required string TerminalOrderId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
