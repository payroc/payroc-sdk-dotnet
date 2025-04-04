using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PayrocCloud.PaymentInstructions;

public record GetPaymentInstructionsRequest
{
    /// <summary>
    /// Unique identifier of the payment instruction.
    /// </summary>
    [JsonIgnore]
    public required string PaymentInstructionId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
