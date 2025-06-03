using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

public record ListTerminalOrdersProcessingAccountsRequest
{
    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingAccountId { get; set; }

    [JsonIgnore]
    public ListTerminalOrdersProcessingAccountsRequestStatus? Status { get; set; }

    [JsonIgnore]
    public DateTime? FromDateTime { get; set; }

    [JsonIgnore]
    public DateTime? ToDateTime { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
