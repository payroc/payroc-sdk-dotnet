using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the settlement.
/// </summary>
[Serializable]
public record SettledSummary : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Processor that settled the transaction.
    /// </summary>
    [JsonPropertyName("settledBy")]
    public string? SettledBy { get; set; }

    /// <summary>
    /// Date that the processor settled the transaction. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("achDate")]
    public DateOnly? AchDate { get; set; }

    /// <summary>
    /// Unique identifier of the ACH deposit.
    /// </summary>
    [JsonPropertyName("achDepositId")]
    public int? AchDepositId { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
