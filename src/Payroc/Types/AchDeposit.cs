using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the ACH deposit.
/// </summary>
[Serializable]
public record AchDeposit : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
    /// Date that the ACH deposit was processed. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("achDate")]
    public DateOnly? AchDate { get; set; }

    /// <summary>
    /// Date that the payment was made. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public DateOnly? PaymentDate { get; set; }

    /// <summary>
    /// Number of transactions included in the ACH deposit.
    /// </summary>
    [JsonPropertyName("transactions")]
    public int? Transactions { get; set; }

    /// <summary>
    /// Total value of sales. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("sales")]
    public int? Sales { get; set; }

    /// <summary>
    /// Total value of returns.
    /// </summary>
    [JsonPropertyName("returns")]
    public int? Returns { get; set; }

    /// <summary>
    /// Total value of daily Fees.
    /// </summary>
    [JsonPropertyName("dailyFees")]
    public int? DailyFees { get; set; }

    /// <summary>
    /// Total value of sales held for risk investigation.
    /// </summary>
    [JsonPropertyName("heldSales")]
    public int? HeldSales { get; set; }

    /// <summary>
    /// Total value of adjustments made to the ACH deposit.
    /// </summary>
    [JsonPropertyName("achAdjustment")]
    public int? AchAdjustment { get; set; }

    /// <summary>
    /// Total value of funds witheld from the settled batch.
    /// </summary>
    [JsonPropertyName("holdback")]
    public int? Holdback { get; set; }

    /// <summary>
    /// Total value of funds released from a hold.
    /// </summary>
    [JsonPropertyName("reserveRelease")]
    public int? ReserveRelease { get; set; }

    /// <summary>
    /// Net ACH deposit value.
    /// </summary>
    [JsonPropertyName("netAmount")]
    public int? NetAmount { get; set; }

    [JsonPropertyName("merchant")]
    public MerchantSummary? Merchant { get; set; }

    [JsonPropertyName("links")]
    public IEnumerable<Link>? Links { get; set; }

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
