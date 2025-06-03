using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the ACH deposit fee.
/// </summary>
public record AchDepositFee
{
    /// <summary>
    /// Unique identifier of the ACH deposit.
    /// </summary>
    [JsonPropertyName("achDepositId")]
    public int? AchDepositId { get; set; }

    /// <summary>
    /// Date that we sent the transaction to the cards brands for clearing. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("associationDate")]
    public DateOnly? AssociationDate { get; set; }

    /// <summary>
    /// Date of the adjustment. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("adjustmentDate")]
    public DateOnly? AdjustmentDate { get; set; }

    /// <summary>
    /// Description of the ACH deposit fee.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Total value of ACH deposit fee.
    /// </summary>
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantSummary? Merchant { get; set; }

    [JsonPropertyName("achDeposit")]
    public AchDepositSummary? AchDeposit { get; set; }

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
