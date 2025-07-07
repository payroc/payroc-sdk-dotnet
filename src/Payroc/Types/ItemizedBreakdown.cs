using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the breakdown of the transaction.
/// </summary>
[Serializable]
public record ItemizedBreakdown
{
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
    /// Total amount of the transaction before tax and fees.
    /// The value is in the currency’s lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("subtotal")]
    public required long Subtotal { get; set; }

    /// <summary>
    /// Value of cashback for the transaction.
    /// </summary>
    [JsonPropertyName("cashbackAmount")]
    public long? CashbackAmount { get; set; }

    /// <summary>
    /// Object that contains tip information for the transaction.
    /// </summary>
    [JsonPropertyName("tip")]
    public Tip? Tip { get; set; }

    /// <summary>
    /// List of taxes.
    /// </summary>
    [JsonPropertyName("taxes")]
    public IEnumerable<Tax>? Taxes { get; set; }

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
