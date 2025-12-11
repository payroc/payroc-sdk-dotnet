using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the tip.
/// </summary>
[Serializable]
public record Tip : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the tip is a fixed amount or a percentage.
    /// **Note:** Our gateway applies the percentage tip to the total amount of the transaction after tax.
    /// </summary>
    [JsonPropertyName("type")]
    public required TipType Type { get; set; }

    /// <summary>
    /// Indicates how the tip was added to the transaction.
    /// - `prompted` – The customer was prompted to add a tip during payment.
    /// - `adjusted` – The customer added a tip on the receipt for the merchant to adjust post-transaction.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("mode")]
    public TipMode? Mode { get; set; }

    /// <summary>
    /// If the value for type is `fixedAmount`, this value is the tip amount in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>
    /// If the value for type is `percentage`, this value is the tip as a percentage.
    /// </summary>
    [JsonPropertyName("percentage")]
    public double? Percentage { get; set; }

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
