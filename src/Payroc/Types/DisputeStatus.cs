using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the current status of the dispute.
/// </summary>
[Serializable]
public record DisputeStatus : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the status of the dispute.
    /// </summary>
    [JsonPropertyName("disputeStatusId")]
    public int? DisputeStatusId { get; set; }

    /// <summary>
    /// Status of the dispute.
    ///
    /// **Note:** If you want to view the status history of the dispute, use our [List Dispute Statuses](https://docs.payroc.com/api/schema/reporting/settlement/list-disputes-statuses) method.
    /// </summary>
    [JsonPropertyName("status")]
    public DisputeStatusStatus? Status { get; set; }

    /// <summary>
    /// Date that the status of the dispute was last changed. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("statusDate")]
    public DateOnly? StatusDate { get; set; }

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
