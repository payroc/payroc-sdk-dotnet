using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PayrocCloud.RefundInstructions;

public record GetRefundInstructionsRequest
{
    /// <summary>
    /// Unique identifier of the refund instruction.
    /// </summary>
    [JsonIgnore]
    public required string RefundInstructionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
