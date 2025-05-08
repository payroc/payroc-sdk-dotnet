using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the total funds available to the merchant.
/// </summary>
public record MerchantBalance
{
    /// <summary>
    /// Unique Identifier of the merchant.
    /// </summary>
    [JsonPropertyName("merchantId")]
    public string? MerchantId { get; set; }

    /// <summary>
    /// Value of funds in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("funds")]
    public int? Funds { get; set; }

    /// <summary>
    /// Value of funds that we have not yet sent to funding recipients. We return this value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("pending")]
    public double? Pending { get; set; }

    /// <summary>
    /// Value of funds available for funding instructions. We return this value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("available")]
    public double? Available { get; set; }

    /// <summary>
    /// Currency of the values. The default value is USD.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

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
