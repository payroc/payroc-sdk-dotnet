using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

[Serializable]
public record GetMerchantPlatformsRequest
{
    /// <summary>
    /// Unique identifier of the merchant platform that we sent to you when you created the merchant platform.
    /// </summary>
    [JsonIgnore]
    public required string MerchantPlatformId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
