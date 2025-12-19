using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

[Serializable]
public record ListBoardingMerchantPlatformProcessingAccountsRequest
{
    /// <summary>
    /// Unique identifier of the merchant platform that we sent to you when you created the merchant platform.
    /// </summary>
    [JsonIgnore]
    public required string MerchantPlatformId { get; set; }

    /// <summary>
    /// Return the previous page of results before the value that you specify.
    ///
    /// You can’t send the before parameter in the same request as the after parameter.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Return the next page of results after the value that you specify.
    ///
    /// You can’t send the after parameter in the same request as the before parameter.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// Limit the maximum number of results that we return for each page.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Indicates if you want to return closed processing accounts. This includes processing accounts that have a status of `terminated`, `cancelled`, or `rejected`.
    /// **Note**: By default, we return only open processing accounts.
    /// </summary>
    [JsonIgnore]
    public bool? IncludeClosed { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
