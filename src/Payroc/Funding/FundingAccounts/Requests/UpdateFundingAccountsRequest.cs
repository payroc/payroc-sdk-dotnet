using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingAccounts;

[Serializable]
public record UpdateFundingAccountsRequest
{
    /// <summary>
    /// Unique identifier of the funding account.
    /// </summary>
    [JsonIgnore]
    public required int FundingAccountId { get; set; }

    [JsonIgnore]
    public required FundingAccount Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
