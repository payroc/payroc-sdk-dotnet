using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains shareable assets for the payment link.
/// </summary>
[Serializable]
public record PaymentLinkAssets : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
