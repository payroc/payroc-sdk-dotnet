using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the total funds available in the card.
/// </summary>
public record CardBalance
{
    /// <summary>
    /// Indicates if the balance relates to an EBT Cash account or EBT SNAP account.
    /// - `cash` – EBT Cash
    /// - `foodStamp` – EBT SNAP
    /// </summary>
    [JsonPropertyName("benefitCategory")]
    public required CardBalanceBenefitCategory BenefitCategory { get; set; }

    /// <summary>
    /// Current balance of the account. This value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    public required long Amount { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

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
