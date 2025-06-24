using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains shareable assets for the payment link.
/// </summary>
[Serializable]
public record PaymentLinkAssets
{
    /// <summary>
    /// URL of the payment link.
    /// </summary>
    [JsonPropertyName("paymentUrl")]
    public required string PaymentUrl { get; set; }

    /// <summary>
    /// HTML code for the payment link. You can embed the HTML code in the merchant's website.
    /// </summary>
    [JsonPropertyName("paymentButton")]
    public required string PaymentButton { get; set; }

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
