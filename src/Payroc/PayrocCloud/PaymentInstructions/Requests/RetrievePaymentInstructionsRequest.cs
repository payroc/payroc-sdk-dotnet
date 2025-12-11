using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PayrocCloud.PaymentInstructions;

[Serializable]
public record RetrievePaymentInstructionsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the payment instruction.
    /// </summary>
    [JsonIgnore]
    public required string PaymentInstructionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
