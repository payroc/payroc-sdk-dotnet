using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the transaction.
/// </summary>
[Serializable]
public record Transaction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier of the transaction. If we can't match a dispute to a transaction, we don't return 'transactionID' or a 'link' object.
    /// </summary>
    [JsonPropertyName("transactionId")]
    public int? TransactionId { get; set; }

    /// <summary>
    /// Indicates the type of transaction.
    /// </summary>
    [JsonPropertyName("type")]
    public TransactionType? Type { get; set; }

    /// <summary>
    /// Date of the transaction. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("date")]
    public DateOnly? Date { get; set; }

    /// <summary>
    /// Transaction amount. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }

    /// <summary>
    /// Describes how the merchant received the payment details. If we can't match a dispute to a transaction, we don't return an 'entryMethod' object.
    /// </summary>
    [JsonPropertyName("entryMethod")]
    public TransactionEntryMethod? EntryMethod { get; set; }

    /// <summary>
    /// Date that we received the transaction.  The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public DateOnly? CreatedDate { get; set; }

    /// <summary>
    /// Date that the transaction was last changed.  The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public DateOnly? LastModifiedDate { get; set; }

    /// <summary>
    /// Indicates the status of the transaction.
    /// </summary>
    [JsonPropertyName("status")]
    public TransactionStatus? Status { get; set; }

    /// <summary>
    /// Cashback amount. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("cashbackAmount")]
    public int? CashbackAmount { get; set; }

    /// <summary>
    /// Object that contains information about the interchange fees for the transaction.
    /// </summary>
    [JsonPropertyName("interchange")]
    public TransactionInterchange? Interchange { get; set; }

    /// <summary>
    /// Currency of the transaction.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantSummary? Merchant { get; set; }

    [JsonPropertyName("settled")]
    public SettledSummary? Settled { get; set; }

    [JsonPropertyName("batch")]
    public BatchSummary? Batch { get; set; }

    [JsonPropertyName("card")]
    public CardSummary? Card { get; set; }

    [JsonPropertyName("authorization")]
    public AuthorizationSummary? Authorization { get; set; }

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
