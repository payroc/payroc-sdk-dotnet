using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record ProcessingCardAcceptanceSpecialityCardsAmericanExpressDirect
{
    /// <summary>
    /// Indicates if the merchant accepts American Express Direct.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    /// <summary>
    /// If the merchant accepts American Express Direct, provide their American Express merchant number.
    /// </summary>
    [JsonPropertyName("merchantNumber")]
    public string? MerchantNumber { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
