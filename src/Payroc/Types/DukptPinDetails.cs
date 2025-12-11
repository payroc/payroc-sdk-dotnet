using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about encrypted PIN details.
/// </summary>
[Serializable]
public record DukptPinDetails : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Encrypted PIN.
    /// **Note:** PIN is encrypted using the DUKPT scheme.
    /// </summary>
    [JsonPropertyName("pin")]
    public required string Pin { get; set; }

    /// <summary>
    /// Key serial number.
    /// </summary>
    [JsonPropertyName("pinKsn")]
    public required string PinKsn { get; set; }

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
