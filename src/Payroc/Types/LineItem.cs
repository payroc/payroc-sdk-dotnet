using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// List of line items.
/// </summary>
[Serializable]
public record LineItem
{
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
    public required double UnitPrice { get; set; }

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

    /// <summary>
    /// List of taxes to be applied to the item.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<Tax>? Taxes { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
