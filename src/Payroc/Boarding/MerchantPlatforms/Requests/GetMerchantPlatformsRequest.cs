using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

public record GetMerchantPlatformsRequest
{
    /// <summary>
    /// Unique identifier of the merchant platform.
    /// </summary>
    [JsonIgnore]
    public required string MerchantPlatformId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
