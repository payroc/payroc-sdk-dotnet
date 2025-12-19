using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PayrocCloud.SignatureInstructions;

[Serializable]
public record DeleteSignatureInstructionsRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the signature instruction.
    /// </summary>
    [JsonIgnore]
    public required string SignatureInstructionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
