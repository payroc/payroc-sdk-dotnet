using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

[Serializable]
public record ListFundingRecipientFundingAccountsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the funding recipient.
    /// </summary>
    [JsonIgnore]
    public required int RecipientId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
