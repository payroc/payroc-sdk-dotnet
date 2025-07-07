using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Electronic Benefit Transfer (EBT) transaction.
/// </summary>
[Serializable]
public record EbtDetails
{
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
