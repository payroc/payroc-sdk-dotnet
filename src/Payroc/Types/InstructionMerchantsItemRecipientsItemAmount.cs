using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the funds that we send to the funding account.
/// </summary>
[Serializable]
public record InstructionMerchantsItemRecipientsItemAmount : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Amount of funds in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("value")]
    public required int Value { get; set; }

    /// <summary>
    /// Currency of the value parameter.
    /// </summary>
    [JsonPropertyName("currency")]
    public InstructionMerchantsItemRecipientsItemAmountCurrency? Currency { get; set; }

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
