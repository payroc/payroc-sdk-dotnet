using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record FundingRecipientFundingAccountsItem
{
    /// <summary>
    /// Unique identifier of the funding account.
    /// </summary>
    [JsonPropertyName("fundingAccountId")]
    public int? FundingAccountId { get; set; }

    /// <summary>
    /// Status of the funding account.
    /// </summary>
    [JsonPropertyName("status")]
    public FundingRecipientFundingAccountsItemStatus? Status { get; set; }

    /// <summary>
    /// HATEOAS links for viewing the funding account.
    /// </summary>
    [JsonPropertyName("link")]
    public FundingRecipientFundingAccountsItemLink? Link { get; set; }

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
