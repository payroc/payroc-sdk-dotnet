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
    /// Unique identifier that we assigned to the transaction.
    /// </summary>
    [JsonPropertyName("transactionId")]
    public int? TransactionId { get; set; }

    /// <summary>
    /// Indicates the type of transaction. The value is one of the following:
    ///
    /// - `capture` - Transaction is a sale.
    /// - `return` - Transaction is a refund.
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
    /// Indicates how the merchant received the payment details.
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
    /// Indicates the status of the transaction. The value is one of the following:
    ///
    /// -	`fullSuspense` – Merchant ran the transaction while their account was in full suspense.
    /// -	`heldAudited` – We have moved a transaction from fullSuspense and placed it on hold.
    /// -	`heldReleasedAudited` – We audited and released the transaction that we had previously held.
    /// -	`holdForSettlement30Days` - We are holding the transaction for a maximum of 30 days.
    /// -	`holdForSettlementDuplicate` - We held the transaction because the transaction may be a duplicate.
    /// -	`holdLongTerm` - We are holding the transaction for an extended period.
    /// -	`paid` – We have paid the transaction funds to the merchant.
    /// -	`paidByThirdParty` - A third party has paid the transaction funds to the merchant.
    /// -	`partialRelease` – We partially released the transaction funds.
    /// -	`pull` - We pulled the transaction, and the merchant does not receive funds for the transaction.
    /// -	`release` - We released the transaction that we previously held.
    /// -	`new` – We have prepared the funds from the transaction to send to the merchant.
    /// -	`held` – We held the transaction.
    /// -	`unknown` – No transaction status available.
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
    /// Currency of the transaction. The value for the currency follows the [ISO 4217](https://www.iso.org/iso-4217-currency-codes.html) standard.
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
