using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PayrocCloud.RefundInstructions;

[Serializable]
public record RetrieveRefundInstructionsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the refund instruction.
    /// </summary>
    [JsonIgnore]
    public required string RefundInstructionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
