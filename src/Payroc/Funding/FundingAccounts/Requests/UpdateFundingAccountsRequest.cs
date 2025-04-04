using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingAccounts;

public record UpdateFundingAccountsRequest
{
    /// <summary>
    /// Unique identifier of the funding account.
    /// </summary>
    [JsonIgnore]
    public required int FundingAccountId { get; set; }

    [JsonIgnore]
    public required FundingAccount Body { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
