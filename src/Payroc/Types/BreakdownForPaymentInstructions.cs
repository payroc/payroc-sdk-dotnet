using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record BreakdownForPaymentInstructions : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// List of taxes.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<TaxRate>? Taxes { get; set; }

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
