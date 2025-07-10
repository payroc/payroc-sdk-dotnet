using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Electronic Benefit Transfer (EBT) transaction.
/// </summary>
[Serializable]
public record EbtDetailsWithVoucher : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("voucher")]
    public Voucher? Voucher { get; set; }

    /// <summary>
    /// Indicates if the balance relates to an EBT Cash account or an EBT SNAP account.
    ///  - `cash` – EBT Cash
    ///  - `foodStamp` – EBT SNAP
    /// </summary>
    [JsonPropertyName("benefitCategory")]
    public required EbtDetailsBenefitCategory BenefitCategory { get; set; }

    /// <summary>
    /// Indicates whether the customer wants to withdraw cash.
    ///
    /// **Note:** Cash withdrawals are available only from EBT Cash accounts.
    /// </summary>
    [JsonPropertyName("withdrawal")]
    public bool? Withdrawal { get; set; }

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
