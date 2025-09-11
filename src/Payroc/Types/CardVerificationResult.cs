using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record CardVerificationResult : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Operator who requested to verify the card.
    /// </summary>
    [JsonPropertyName("operator")]
    public string? Operator { get; set; }

    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("card")]
    public Card? Card { get; set; }

    /// <summary>
    /// Indicates if the card details are valid:
    ///
    /// - `true` - Card details are valid.
    /// - `false` - Card details are not valid.
    /// </summary>
    [JsonPropertyName("verified")]
    public required bool Verified { get; set; }

    [JsonPropertyName("transactionResult")]
    public TransactionResult? TransactionResult { get; set; }

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
