using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the encrypted card data for keyed transactions.
/// </summary>
[Serializable]
public record FullyEncryptedKeyedDataFormat : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("device")]
    public required EncryptionCapableDevice Device { get; set; }

    /// <summary>
    /// Encrypted card data.
    /// </summary>
    [JsonPropertyName("encryptedData")]
    public required string EncryptedData { get; set; }

    /// <summary>
    /// First digit of the customer’s card number.
    /// </summary>
    [JsonPropertyName("firstDigitOfPan")]
    public string? FirstDigitOfPan { get; set; }

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
