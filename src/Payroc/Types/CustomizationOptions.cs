using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains available options to customize certain aspects of an instruction.
/// </summary>
[Serializable]
public record CustomizationOptions : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("ebtDetails")]
    public EbtDetails? EbtDetails { get; set; }

    /// <summary>
    /// Indicates how you want the device to capture the card details.
    /// - `deviceRead` - Device prompts the cardholder to tap, swipe, or insert their card.
    /// - `manualEntry` - Device prompts the merchant or cardholder to manually enter card details.
    /// - `deviceReadOrManualEntry` - Device prompts the cardholder to tap, swipe, or insert their card. The device also displays an option for the merchant or cardholder to manually enter card details.
    /// </summary>
    [JsonPropertyName("entryMethod")]
    public CustomizationOptionsEntryMethod? EntryMethod { get; set; }

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
