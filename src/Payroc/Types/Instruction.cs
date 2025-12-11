using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Inform the payfac what to do with the specified funds. **
/// </summary>
[Serializable]
public record Instruction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the funding instruction.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("instructionId")]
    public int? InstructionId { get; set; }

    /// <summary>
    /// Date that we created the funding instruction. The date format follows the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) standard.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("createdDate")]
    public string? CreatedDate { get; set; }

    /// <summary>
    /// Date of the most recent change to the funding instruction. The date format follows the [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) standard.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("lastModifiedDate")]
    public string? LastModifiedDate { get; set; }

    /// <summary>
    /// Status of the funding instruction. Our gateway returns one of the following values:
    /// - `accepted` - We have received the funding instruction but have not yet reviewed it.
    /// - `pending` - We have received the funding instruction and we are reviewing it.
    /// - `completed` - We have reviewed and processed the funding instruction.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public InstructionStatus? Status { get; set; }

    /// <summary>
    /// Array of merchants objects. Each object specifies the merchant whose funding balance we distribute and who you want to send the funds to.
    /// </summary>
    [JsonPropertyName("merchants")]
    public IEnumerable<InstructionMerchantsItem>? Merchants { get; set; }

    /// <summary>
    /// [Metadata](https://docs.payroc.com/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
