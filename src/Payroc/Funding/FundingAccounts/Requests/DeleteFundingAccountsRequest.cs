using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingAccounts;

public record DeleteFundingAccountsRequest
{
    /// <summary>
    /// Unique identifier of the funding account.
    /// </summary>
    [JsonIgnore]
    public required int FundingAccountId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
