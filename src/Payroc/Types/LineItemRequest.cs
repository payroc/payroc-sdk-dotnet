using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record LineItemRequest : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Array of objects that contain information about each tax that applies to the item.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<Tax>? Taxes { get; set; }

    /// <summary>
    /// Commodity code of the item.
    /// </summary>
    [JsonPropertyName("commodityCode")]
    public string? CommodityCode { get; set; }

    /// <summary>
    /// Product code of the item.
    /// </summary>
    [JsonPropertyName("productCode")]
    public string? ProductCode { get; set; }

    /// <summary>
    /// Description of the item.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("unitOfMeasure")]
    public UnitOfMeasure? UnitOfMeasure { get; set; }

    /// <summary>
    /// Price of each unit.
    /// </summary>
    [JsonPropertyName("unitPrice")]
    public required long UnitPrice { get; set; }

    /// <summary>
    /// Number of units.
    /// </summary>
    [JsonPropertyName("quantity")]
    public required double Quantity { get; set; }

    /// <summary>
    /// Discount rate that the merchant applies to the item.
    /// </summary>
    [JsonPropertyName("discountRate")]
    public double? DiscountRate { get; set; }

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
