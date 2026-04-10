using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record ClosedLoopResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the closed-loop read.
    /// </summary>
    [JsonPropertyName("closedLoopReadId")]
    public required string ClosedLoopReadId { get; set; }

    /// <summary>
    /// Date that the payment device read the closed-loop card. Our gateway returns this value in **YYYY-MM-DD** format.
    /// </summary>
    [JsonPropertyName("readDate")]
    public required DateOnly ReadDate { get; set; }

    /// <summary>
    /// Unstructured payload from the card.
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, object?> Data { get; set; } = new Dictionary<string, object?>();

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
