using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the fees.
/// </summary>
[Serializable]
public record RewardPayChoiceFees : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Fee for the monthly subscription. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("monthlySubscription")]
    public required int MonthlySubscription { get; set; }

    /// <summary>
    /// Object that contains information about fees for debit transactions.
    /// </summary>
    [JsonPropertyName("debit")]
    public required RewardPayChoiceFeesDebit Debit { get; set; }

    /// <summary>
    /// Object that contains information about fees for credit transactions.
    /// </summary>
    [JsonPropertyName("credit")]
    public required RewardPayChoiceFeesCredit Credit { get; set; }

    [JsonPropertyName("specialityCards")]
    public SpecialityCards? SpecialityCards { get; set; }

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
