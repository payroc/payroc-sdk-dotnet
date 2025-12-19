using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the target funding account.
/// </summary>
[Serializable]
public record InstructionMerchantsItemRecipientsItem : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the funding account.
    /// </summary>
    [JsonPropertyName("fundingAccountId")]
    public required int FundingAccountId { get; set; }

    /// <summary>
    /// Payment method that we use to send funds to the funding account.
    /// </summary>
    [JsonPropertyName("paymentMethod")]
    public required InstructionMerchantsItemRecipientsItemPaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Object that contains information about the funds that we send to the funding account.
    /// </summary>
    [JsonPropertyName("amount")]
    public required InstructionMerchantsItemRecipientsItemAmount Amount { get; set; }

    /// <summary>
    /// Status of the individual payment instruction. Our gateway returns one of the following values:
    /// -	`accepted` - We received the payment instruction, but we haven't reviewed it.
    /// -	`pending` - We are reviewing the payment instruction.
    /// -	`released` - We approved the payment instruction.
    /// -	`funded` - We sent the funds to the funding account by ACH.
    /// -	`failed` - The ACH payment to the funding account failed.
    /// -	`rejected` - We reviewed the payment instruction and rejected it.
    /// - `onHold` - We have placed the payment instruction on hold.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("status")]
    public InstructionMerchantsItemRecipientsItemStatus? Status { get; set; }

    /// <summary>
    /// [Metadata](https://docs.payroc.com/api/metadata) object you can use to include custom data with your request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    /// Object that contains HATEOAS links for the resource.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("link")]
    public InstructionMerchantsItemRecipientsItemLink? Link { get; set; }

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
