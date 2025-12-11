using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingInstructions;

[Serializable]
public record RetrieveFundingInstructionsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the funding instruction.
    /// </summary>
    [JsonIgnore]
    public required int InstructionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
