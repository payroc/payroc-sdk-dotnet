using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record DisputeCurrentStatus : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

    /// <summary>
    /// Unique identifier of the dispute status.
    /// </summary>
    [JsonPropertyName("disputeStatusId")]
    public int? DisputeStatusId { get; set; }

    /// <summary>
    /// Status of the dispute.
    /// </summary>
    [JsonPropertyName("status")]
    public DisputeStatusStatus? Status { get; set; }

    /// <summary>
    /// Date that the status changed. The format of this value is **YYYY-MM-DD**.
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
