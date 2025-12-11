using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the types of cards that the processing account accepts.
/// </summary>
[Serializable]
public record ProcessingCardAcceptance : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
