using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record AchFees : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Fee for each transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("transaction")]
    public required int Transaction { get; set; }

    /// <summary>
    /// Fee for each batch. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("batch")]
    public required int Batch { get; set; }

    /// <summary>
    /// Fee for each return. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("returns")]
    public required int Returns { get; set; }

    /// <summary>
    /// Fee for each unauthorized return. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("unauthorizedReturn")]
    public required int UnauthorizedReturn { get; set; }

    /// <summary>
    /// Fee for each statement. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("statement")]
    public required int Statement { get; set; }

    /// <summary>
    /// Minimum monthly value of transactions. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("monthlyMinimum")]
    public required int MonthlyMinimum { get; set; }

    /// <summary>
    /// Fee for each account verification. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("accountVerification")]
    public required int AccountVerification { get; set; }

    /// <summary>
    /// Percentage discount for ACH transfers less than $10,000.
    /// </summary>
    [JsonPropertyName("discountRateUnder10000")]
    public required double DiscountRateUnder10000 { get; set; }

    /// <summary>
    /// Percentage discount for ACH transfers equal to or more than $10,000.
    /// </summary>
    [JsonPropertyName("discountRateAbove10000")]
    public required double DiscountRateAbove10000 { get; set; }

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
