using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains summary information about the transaction that the dispute is linked to.
/// </summary>
[Serializable]
public record TransactionSummary : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier of the transaction. If we can't match a dispute to a transaction, we don't return the transactionId or link object.
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
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Describes how the merchant received the payment details. If we can't match a dispute to a transaction, we don't return an entryMethod object.
    /// </summary>
    [JsonPropertyName("entryMethod")]
    public TransactionSummaryEntryMethod? EntryMethod { get; set; }

    /// <summary>
    /// Total amount of the transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
