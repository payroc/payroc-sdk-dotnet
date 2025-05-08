using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the surcharge.
/// </summary>
public record Surcharge
{
    /// <summary>
    /// Indicates if the merchant wants to remove the surcharge fee from the transaction.
    /// `true` - The gateway removes the surcharge fee from the transaction.
    /// `false` - The gateway adds the fee to the transaction.
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
