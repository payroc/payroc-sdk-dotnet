using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

public record ListProcessingAccountOwnersRequest
{
    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingAccountId { get; set; }

    /// <summary>
    /// Points to the resource identifier that you want to receive your results before. Typically, this is the first resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Points to the resource identifier that you want to receive your results after. Typically, this is the last resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// States the total amount of results the response is limited to.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
