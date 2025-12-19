using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record FundingRecipientFundingAccountsItem : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
    /// Object that contains HATEOAS links for the resource.
    /// </summary>
    [JsonPropertyName("link")]
    public FundingRecipientFundingAccountsItemLink? Link { get; set; }

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
