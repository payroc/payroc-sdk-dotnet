using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the types of transactions ran by the processing account. The percentages for transaction types must total 100%.
/// </summary>
[Serializable]
public record ProcessingVolumeBreakdown
{
    /// <summary>
    /// Estimated percentage of keyed card-present transactions.
    /// </summary>
    [JsonPropertyName("cardPresentKeyed")]
    public required int CardPresentKeyed { get; set; }

    /// <summary>
    /// Estimated percentage of swiped card-present transactions.
    /// </summary>
    [JsonPropertyName("cardPresentSwiped")]
    public required int CardPresentSwiped { get; set; }

    /// <summary>
    /// Estimated percentage of mail order or telephone transactions.
    /// </summary>
    [JsonPropertyName("mailOrTelephone")]
    public required int MailOrTelephone { get; set; }

    /// <summary>
    /// Esimated percentage of e-Commerce transactions.
    /// </summary>
    [JsonPropertyName("ecommerce")]
    public required int Ecommerce { get; set; }

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
