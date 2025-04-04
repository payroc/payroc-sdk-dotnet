using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

public record GetProcessingAccountsRequest
{
    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingAccountId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
