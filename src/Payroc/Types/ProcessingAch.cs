using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about Automated Clearing House (ACH) transactions.
/// </summary>
[Serializable]
public record ProcessingAch : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// North American Industry Classification System (NAICS) code.
    /// </summary>
    [JsonPropertyName("naics")]
    public string? Naics { get; set; }

    /// <summary>
    /// Indicates if the business or its principals were previously turned down for ACH processing.
    /// </summary>
    [JsonPropertyName("previouslyTerminatedForAch")]
    public bool? PreviouslyTerminatedForAch { get; set; }

    /// <summary>
    /// Object that contains information about the ACH refund policy for the processing account.
    /// </summary>
    [JsonPropertyName("refunds")]
    public required ProcessingAchRefunds Refunds { get; set; }

    /// <summary>
    /// Estimated maximum number of transactions that the merchant will process in a month.
    /// </summary>
    [JsonPropertyName("estimatedMonthlyTransactions")]
    public required int EstimatedMonthlyTransactions { get; set; }

    /// <summary>
    /// Object that contains information about transaction limits for the processing account.
    /// </summary>
    [JsonPropertyName("limits")]
    public required ProcessingAchLimits Limits { get; set; }

    /// <summary>
    /// List of transaction types that the processing account supports.
    /// </summary>
    [JsonPropertyName("transactionTypes")]
    public IEnumerable<ProcessingAchTransactionTypesItem>? TransactionTypes { get; set; }

    /// <summary>
    /// If you send a value of `other` for transactionTypes, provide a list of the supported transaction types.
    /// </summary>
    [JsonPropertyName("transactionTypesOther")]
    public string? TransactionTypesOther { get; set; }

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
