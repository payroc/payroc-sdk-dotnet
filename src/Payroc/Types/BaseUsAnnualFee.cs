using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the annual fee.
/// </summary>
[Serializable]
public record BaseUsAnnualFee : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates whether we collect the annual fee in June or December.
    /// </summary>
    [JsonPropertyName("billInMonth")]
    public BaseUsAnnualFeeBillInMonth? BillInMonth { get; set; }

    /// <summary>
    /// Annual fee. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required int Amount { get; set; }

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
