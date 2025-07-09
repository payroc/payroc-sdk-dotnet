using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the monthly processing amounts for the processing account.
/// </summary>
[Serializable]
public record ProcessingMonthlyAmounts : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Estimated average transaction amount each month. The value is in the currency's lowest denomination, for example, cents. You must provide an amount that is greater than zero.
    /// </summary>
    [JsonPropertyName("average")]
    public required int Average { get; set; }

    /// <summary>
    /// Estimated maximum transaction amount each month. The value is in the currency's lowest denomination, for example, cents. You must provide an amount that is greater than zero.
    /// </summary>
    [JsonPropertyName("highest")]
    public required int Highest { get; set; }

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
