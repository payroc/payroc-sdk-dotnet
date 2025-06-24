using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the password settings when running specific transaction types.
/// </summary>
[Serializable]
public record OrderItemSolutionSetupApplicationSettingsSecurity
{
    /// <summary>
    /// Indicates if the terminal should prompt the clerk for a password when running a refund.
    /// </summary>
    [JsonPropertyName("refundPassword")]
    public bool? RefundPassword { get; set; }

    /// <summary>
    /// Indicates if the terminal should prompt the clerk for a password when running a keyed sale.
    /// </summary>
    [JsonPropertyName("keyedSalePassword")]
    public bool? KeyedSalePassword { get; set; }

    /// <summary>
    /// Indicates if the terminal should prompt the clerk for a password when cancelling a transaction.
    /// </summary>
    [JsonPropertyName("reversalPassword")]
    public bool? ReversalPassword { get; set; }

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
