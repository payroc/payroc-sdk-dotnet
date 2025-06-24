using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Information around the type of cards that will be accepted.
/// </summary>
[Serializable]
public record ProcessingCardAcceptance
{
    /// <summary>
    /// Indicates if the merchant accepts only debit cards.
    /// </summary>
    [JsonPropertyName("debitOnly")]
    public bool? DebitOnly { get; set; }

    /// <summary>
    /// Indicates if the merchant accepts health savings account (HSA) and flexible spending account (FSA) cards.
    /// </summary>
    [JsonPropertyName("hsaFsa")]
    public bool? HsaFsa { get; set; }

    /// <summary>
    /// List of card types the merchant accepts.
    /// </summary>
    [JsonPropertyName("cardsAccepted")]
    public IEnumerable<ProcessingCardAcceptanceCardsAcceptedItem>? CardsAccepted { get; set; }

    /// <summary>
    /// Information about the speciality cards that the merchant accepts.
    /// </summary>
    [JsonPropertyName("specialityCards")]
    public ProcessingCardAcceptanceSpecialityCards? SpecialityCards { get; set; }

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
