using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the target funding account.
/// </summary>
public record InstructionMerchantsItemRecipientsItem
{
    /// <summary>
    /// Unique identifier of the funding account that we pay the funds into.
    /// </summary>
    [JsonPropertyName("fundingAccountId")]
    public required int FundingAccountId { get; set; }

    /// <summary>
    /// Method of payment.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public string PaymentMethod { get; set; } = "ACH";

    /// <summary>
    /// Object that contains details about the funds.
    /// </summary>
    [JsonPropertyName("amount")]
    public required InstructionMerchantsItemRecipientsItemAmount Amount { get; set; }

    /// <summary>
    /// Status of the funding instruction.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public InstructionMerchantsItemRecipientsItemStatus? Status { get; set; }

    /// <summary>
    /// [Metadata](/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Array of HATEOAS links for viewing a funding account.
    /// </summary>
    [JsonPropertyName("link")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public InstructionMerchantsItemRecipientsItemLink? Link { get; set; }

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
