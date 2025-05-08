using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record ProcessingCardAcceptanceSpecialityCardsOther
{
    /// <summary>
    /// If the merchant accepts WEX, provide their WEX merchant number.
    /// </summary>
    [JsonPropertyName("wexMerchantNumber")]
    public string? WexMerchantNumber { get; set; }

    /// <summary>
    /// If the merchant accepts Voyager, provide their Voyager merchant ID.
    /// </summary>
    [JsonPropertyName("voyagerMerchantId")]
    public string? VoyagerMerchantId { get; set; }

    /// <summary>
    /// If the merchant accepts Fleet, provide their Fleet merchant ID.
    /// </summary>
    [JsonPropertyName("fleetMerchantId")]
    public string? FleetMerchantId { get; set; }

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
