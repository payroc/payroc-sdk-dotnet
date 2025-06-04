using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the dispute.
/// </summary>
public record Dispute
{
    /// <summary>
    /// Unique identifier of the dispute.
    /// </summary>
    [JsonPropertyName("disputeId")]
    public int? DisputeId { get; set; }

    /// <summary>
    /// Type of dispute.
    /// </summary>
    [JsonPropertyName("disputeType")]
    public DisputeDisputeType? DisputeType { get; set; }

    [JsonPropertyName("currentStatus")]
    public DisputeCurrentStatus? CurrentStatus { get; set; }

    /// <summary>
    /// Date that we received the dispute. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public DateOnly? CreatedDate { get; set; }

    /// <summary>
    /// Date that the dispute was last changed. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public DateOnly? LastModifiedDate { get; set; }

    /// <summary>
    /// Date that the acquiring bank received the dispute. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("receivedDate")]
    public DateOnly? ReceivedDate { get; set; }

    /// <summary>
    /// Description of the dispute.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Reference number from the acquiring bank.
    /// </summary>
    [JsonPropertyName("referenceNumber")]
    public string? ReferenceNumber { get; set; }

    /// <summary>
    /// Dispute amount. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("disputeAmount")]
    public int? DisputeAmount { get; set; }

    /// <summary>
    /// Value of the fees for the dispute. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("feeAmount")]
    public int? FeeAmount { get; set; }

    /// <summary>
    /// Indicates if this is the first dispute for the transaction.
    /// </summary>
    [JsonPropertyName("firstDispute")]
    public bool? FirstDispute { get; set; }

    /// <summary>
    /// Authorization code.
    /// </summary>
    [JsonPropertyName("authorizationCode")]
    public string? AuthorizationCode { get; set; }

    /// <summary>
    /// Currency of the dispute.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("card")]
    public CardSummary? Card { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantSummary? Merchant { get; set; }

    [JsonPropertyName("transaction")]
    public TransactionSummary? Transaction { get; set; }

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
