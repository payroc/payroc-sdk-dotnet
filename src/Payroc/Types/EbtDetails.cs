using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the Electronic Benefit Transfer (EBT) transaction.
/// </summary>
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
    /// Indicates a request to withdraw cash.
    /// **Note:** Cash withdrawal is available only for EBT Cash benefit accounts.
    /// </summary>
    [JsonPropertyName("withdrawal")]
    public bool? Withdrawal { get; set; }

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
