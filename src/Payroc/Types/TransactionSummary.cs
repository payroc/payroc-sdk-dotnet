using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains summary information about the transaction.
/// </summary>
public record TransactionSummary
{
    /// <summary>
    /// Unique identifier of the transaction. If we can't match a dispute to a transaction, we don't return 'transactionID' or a 'link' object.
    /// </summary>
    [JsonPropertyName("transactionId")]
    public int? TransactionId { get; set; }

    /// <summary>
    /// Indicates the type of transaction.
    /// </summary>
    [JsonPropertyName("type")]
    public TransactionSummaryType? Type { get; set; }

    /// <summary>
    /// Date of the transaction. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    /// <summary>
    /// Describes how the merchant received the payment details. If we can't match a dispute to a transaction, we don't return an 'entryMethod' object.
    /// </summary>
    [JsonPropertyName("entryMethod")]
    public TransactionSummaryEntryMethod? EntryMethod { get; set; }

    /// <summary>
    /// Transaction amount. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
