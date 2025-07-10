using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the ACH deposit fee.
/// </summary>
[Serializable]
public record AchDepositFee : IJsonOnDeserialized
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
