using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

public record ListBoardingMerchantPlatformProcessingAccountsRequest
{
    /// <summary>
    /// Unique identifier of the merchant platform that we sent to you when you created the merchant platform.
    /// </summary>
    [JsonIgnore]
    public required string MerchantPlatformId { get; set; }

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

    /// <summary>
    /// Indicates if you want to return closed processing accounts. This includes processing accounts that have a status of `terminated`, `cancelled`, or `rejected`.
    /// </summary>
    [JsonIgnore]
    public bool? IncludeClosed { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
