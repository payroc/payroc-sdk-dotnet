using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the surcharge.
/// </summary>
[Serializable]
public record Surcharge : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the merchant wants to remove the surcharge fee from the transaction.
    /// - `true` - Gateway removes the surcharge fee from the transaction.
    /// - `false` - Gateway adds the fee to the transaction.
    /// </summary>
    [JsonPropertyName("bypass")]
    public bool? Bypass { get; set; }

    /// <summary>
    /// If the merchant added a surcharge fee, this value indicates the amount of the surcharge fee
    /// in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("amount")]
    public long? Amount { get; set; }

    /// <summary>
    /// If the merchant added a surcharge fee, this value indicates the surcharge percentage.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
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
