using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains available options to customize certain aspects of an instruction.
/// </summary>
public record CustomizationOptions
{
    [JsonPropertyName("ebtDetails")]
    public EbtDetails? EbtDetails { get; set; }

    /// <summary>
    /// Indicates how you want the device to capture the card details.
    /// - `deviceRead` - The cardholder taps, swipes, or inserts their card.
    /// - `manualEntry` - The merchant or cardholder manually enters the card details.
    /// </summary>
    [JsonPropertyName("entryMethod")]
    public CustomizationOptionsEntryMethod? EntryMethod { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
