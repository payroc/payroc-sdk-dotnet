using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the breakdown of the transaction.
/// </summary>
[Serializable]
public record ItemizedBreakdown : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Amount of duties or fees that apply to the order. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("dutyAmount")]
    public long? DutyAmount { get; set; }

    /// <summary>
    /// Amount for shipping in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("freightAmount")]
    public long? FreightAmount { get; set; }

    [JsonPropertyName("convenienceFee")]
    public ConvenienceFee? ConvenienceFee { get; set; }

    /// <summary>
    /// Array of objects that contain information about each item that the customer purchased.
    /// </summary>
    [JsonPropertyName("items")]
    public IEnumerable<LineItem>? Items { get; set; }

    /// <summary>
    /// List of taxes.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<RetrievedTax>? Taxes { get; set; }

    /// <summary>
    /// Amount of the transaction before tax and fees. The value is in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

    /// <summary>
    /// Amount of cashback for the transaction.
    /// </summary>
    [JsonPropertyName("cashbackAmount")]
    public long? CashbackAmount { get; set; }

    /// <summary>
    /// Object that contains tip information for the transaction.
    /// </summary>
    [JsonPropertyName("tip")]
    public Tip? Tip { get; set; }

    /// <summary>
    /// Object that contains surcharge information for the transaction.
    /// </summary>
    [JsonPropertyName("surcharge")]
    public Surcharge? Surcharge { get; set; }

    /// <summary>
    /// Object that contains dual pricing information for the transaction.
    /// </summary>
    [JsonPropertyName("dualPricing")]
    public DualPricing? DualPricing { get; set; }

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
