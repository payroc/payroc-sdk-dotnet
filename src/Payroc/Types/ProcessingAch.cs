using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record ProcessingAch
{
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

    [JsonPropertyName("refunds")]
    public required ProcessingAchRefunds Refunds { get; set; }

    /// <summary>
    /// Estimated maximum number of transactions that the merchant will process in a month.
    /// </summary>
    [JsonPropertyName("estimatedMonthlyTransactions")]
    public required int EstimatedMonthlyTransactions { get; set; }

    [JsonPropertyName("limits")]
    public required ProcessingAchLimits Limits { get; set; }

    /// <summary>
    /// List of supported transaction types.
    /// </summary>
    [JsonPropertyName("transactionTypes")]
    public IEnumerable<ProcessingAchTransactionTypesItem>? TransactionTypes { get; set; }

    /// <summary>
    /// If you send a value of `other` for transactionTypes, provide a list of the supported transaction types.
    /// </summary>
    [JsonPropertyName("transactionTypesOther")]
    public string? TransactionTypesOther { get; set; }

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
