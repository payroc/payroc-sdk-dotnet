using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Batch
{
    /// <summary>
    /// Unique identifier of the batch.
    /// </summary>
    [JsonPropertyName("batchId")]
    public int? BatchId { get; set; }

    /// <summary>
    /// Date that the merchant submitted the batch. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    /// <summary>
    /// Date that we created a record for the batch. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public string? CreatedDate { get; set; }

    /// <summary>
    /// Date that the batch was last changed. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public string? LastModifiedDate { get; set; }

    /// <summary>
    /// Total value of sales. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("saleAmount")]
    public int? SaleAmount { get; set; }

    /// <summary>
    /// Total value of authorizations. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("heldAmount")]
    public int? HeldAmount { get; set; }

    /// <summary>
    /// Total value of returns. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("returnAmount")]
    public int? ReturnAmount { get; set; }

    /// <summary>
    /// Total number of transactions in the batch.
    /// </summary>
    [JsonPropertyName("transactionCount")]
    public int? TransactionCount { get; set; }

    /// <summary>
    /// Currency of the transactions.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantSummary? Merchant { get; set; }

    [JsonPropertyName("links")]
    public IEnumerable<Link>? Links { get; set; }

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
