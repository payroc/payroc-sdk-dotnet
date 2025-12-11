using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

[Serializable]
public record RetrieveProcessingAccountsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the processing account.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingAccountId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
