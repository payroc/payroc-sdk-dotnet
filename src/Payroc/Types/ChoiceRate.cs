using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the choice rate. We return this only if the value for offered was `true`.
/// </summary>
[Serializable]
public record ChoiceRate : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the merchant applies a choice rate to the transaction amount.
    ///
    /// Our gateway adds a choice rate to the transaction when the merchant offers an alternative payment type, but the customer chooses to pay by card.
    /// </summary>
    [JsonPropertyName("applied")]
    public required bool Applied { get; set; }

    /// <summary>
    /// If the customer used a card to pay for the transaction, this value indicates the percentage that our gateway added to the transaction amount.
    /// **Note:** Our gateway returns a value for **rate** only if the value for **applied** in the request is `true`.
    /// </summary>
    [JsonPropertyName("rate")]
    public required double Rate { get; set; }

    /// <summary>
    /// If the customer used a card to pay for the transaction, this value indicates the amount that our gateway added to the transaction amount. This value is in the currency’s lowest denomination, for example, cents.
    /// **Note:** Our gateway returns a value for **amount** only if the value for **applied** in the request is `true`.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

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
