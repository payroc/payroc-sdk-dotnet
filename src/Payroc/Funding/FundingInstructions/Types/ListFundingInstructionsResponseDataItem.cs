using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingInstructions;

public record ListFundingInstructionsResponseDataItem
{
    [JsonPropertyName("link")]
    public Link? Link { get; set; }

    /// <summary>
    /// Unique identifier of the funding instruction.
    /// </summary>
    [JsonPropertyName("instructionId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public int? InstructionId { get; set; }

    /// <summary>
    /// Date that we created the funding instruction.
    /// </summary>
    [JsonPropertyName("createdDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public string? CreatedDate { get; set; }

    /// <summary>
    /// Date of the most recent change to the funding instruction.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public string? LastModifiedDate { get; set; }

    /// <summary>
    /// Status of the funding instruction.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public InstructionStatus? Status { get; set; }

    /// <summary>
    /// Array of merchantInstruction objects.
    /// </summary>
    [JsonPropertyName("merchants")]
    public IEnumerable<InstructionMerchantsItem>? Merchants { get; set; }

    /// <summary>
    /// [Metadata](/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
