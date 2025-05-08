using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingInstructions;

public record GetFundingInstructionsRequest
{
    /// <summary>
    /// Unique identifier of the funding instruction.
    /// </summary>
    [JsonIgnore]
    public required int InstructionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
