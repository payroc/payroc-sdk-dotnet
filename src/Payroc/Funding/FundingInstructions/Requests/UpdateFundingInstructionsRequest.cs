using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingInstructions;

public record UpdateFundingInstructionsRequest
{
    /// <summary>
    /// Unique identifier of the funding instruction.
    /// </summary>
    [JsonIgnore]
    public required int InstructionId { get; set; }

    [JsonIgnore]
    public required Instruction Body { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
