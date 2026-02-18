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
    /// Unique identifier that we assigned to the ACH deposit.
    /// </summary>
    [JsonPropertyName("achDepositId")]
    public int? AchDepositId { get; set; }

    /// <summary>
    /// Date that we sent the transactions to the card brands for clearing. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("associationDate")]
    public DateOnly? AssociationDate { get; set; }

    /// <summary>
    /// Date that we sent the ACH deposit. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("achDate")]
    public DateOnly? AchDate { get; set; }

    /// <summary>
    /// Date that the merchant received the ACH deposit. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("paymentDate")]
    public DateOnly? PaymentDate { get; set; }

    /// <summary>
    /// Number of transactions in the ACH deposit.
    /// </summary>
    [JsonPropertyName("transactions")]
    public int? Transactions { get; set; }

    /// <summary>
    /// Amount of sales in the ACH deposit. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("sales")]
    public long? Sales { get; set; }

    /// <summary>
    /// Amount of returns in the ACH deposit. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("returns")]
    public long? Returns { get; set; }

    /// <summary>
    /// Amount of fees that were applied to the transactions in the ACH deposit. We return the value in the currency's lowest denomination, for example cents.
    /// </summary>
    [JsonPropertyName("dailyFees")]
    public long? DailyFees { get; set; }

    /// <summary>
    /// Amount of funds that we held if the merchant was in full suspense. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("heldSales")]
    public long? HeldSales { get; set; }

    /// <summary>
    /// Amount of adjustments that we made to the ACH deposit. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("achAdjustment")]
    public long? AchAdjustment { get; set; }

    /// <summary>
    /// Amount of funds that we held as reserve from the ACH deposit. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("holdback")]
    public long? Holdback { get; set; }

    /// <summary>
    /// Amount of funds that we released from holdback. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("reserveRelease")]
    public long? ReserveRelease { get; set; }

    /// <summary>
    /// Total amount that we paid the merchant after fees and adjustments. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("netAmount")]
    public long? NetAmount { get; set; }

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
