using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the charge when the merchant enters the amount of the transaction.
/// </summary>
[Serializable]
public record PresetPaymentLinkCharge
{
    /// <summary>
    /// Total amount of the transaction in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

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
