using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about how we process transactions for the account.
/// </summary>
[Serializable]
public record Processing : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that the acquiring platform assigns to the merchant.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("merchantId")]
    public string? MerchantId { get; set; }

    /// <summary>
    /// Object that contains information about transaction amounts for the processing account.
    /// </summary>
    [JsonPropertyName("transactionAmounts")]
    public required ProcessingTransactionAmounts TransactionAmounts { get; set; }

    /// <summary>
    /// Object that contains information about the monthly processing amounts for the processing account.
    /// </summary>
    [JsonPropertyName("monthlyAmounts")]
    public required ProcessingMonthlyAmounts MonthlyAmounts { get; set; }

    /// <summary>
    /// Object that contains information about the types of transactions ran by the processing account. The percentages for transaction types must total 100%.
    /// </summary>
    [JsonPropertyName("volumeBreakdown")]
    public required ProcessingVolumeBreakdown VolumeBreakdown { get; set; }

    /// <summary>
    /// Indicates if the processing account runs transactions on a seasonal basis. For example, if the processing account runs transactions during only the winter months, send a value of `true`.
    /// </summary>
    [JsonPropertyName("isSeasonal")]
    public bool? IsSeasonal { get; set; }

    /// <summary>
    /// Months of the year that the processing account runs transactions.
    /// </summary>
    [JsonPropertyName("monthsOfOperation")]
    public IEnumerable<ProcessingMonthsOfOperationItem>? MonthsOfOperation { get; set; }

    /// <summary>
    /// Object that contains information about Automated Clearing House (ACH) transactions.
    /// </summary>
    [JsonPropertyName("ach")]
    public ProcessingAch? Ach { get; set; }

    /// <summary>
    /// Object that contains information about the types of cards that the processing account accepts.
    /// </summary>
    [JsonPropertyName("cardAcceptance")]
    public ProcessingCardAcceptance? CardAcceptance { get; set; }

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
