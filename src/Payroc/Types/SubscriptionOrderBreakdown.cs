using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the surcharge and taxes that apply to the transaction.
/// </summary>
[Serializable]
public record SubscriptionOrderBreakdown : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Total amount for the transaction before tax. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

    /// <summary>
    /// Array of tax objects.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<Tax>? Taxes { get; set; }

    /// <summary>
    /// Object that contains information about the [surcharge](https://docs.payroc.com/knowledge/card-payments/credit-card-surcharging) that we applied to the transaction.
    /// </summary>
    [JsonPropertyName("surcharge")]
    public Surcharge? Surcharge { get; set; }

    [JsonPropertyName("convenienceFee")]
    public ConvenienceFee? ConvenienceFee { get; set; }

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
