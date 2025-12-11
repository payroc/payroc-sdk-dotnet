using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the tokenization settings and AVS settings for the terminal.
/// </summary>
[Serializable]
public record ProcessingTerminalSecurity : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the terminal can tokenize customer's payment details. For more information about tokenization, go to [Tokenization](https://docs.payroc.com/knowledge/basic-concepts/tokenization).
    /// </summary>
    [JsonPropertyName("tokenization")]
    public required bool Tokenization { get; set; }

    /// <summary>
    /// Indicates if the terminal should prompt for Address Verification Service (AVS) details when running a transaction.
    /// </summary>
    [JsonPropertyName("avsPrompt")]
    public required bool AvsPrompt { get; set; }

    /// <summary>
    /// Indicates the level of AVS details that the terminal should prompt for.
    /// </summary>
    [JsonPropertyName("avsLevel")]
    public ProcessingTerminalSecurityAvsLevel? AvsLevel { get; set; }

    /// <summary>
    /// Indicates if the terminal should prompt for a Card Verfication Value (CVV) when running a transaction.
    /// </summary>
    [JsonPropertyName("cvvPrompt")]
    public required bool CvvPrompt { get; set; }

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
