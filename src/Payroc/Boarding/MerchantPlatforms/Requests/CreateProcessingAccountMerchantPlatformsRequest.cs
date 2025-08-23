using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

[Serializable]
public record CreateProcessingAccountMerchantPlatformsRequest
{
    /// <summary>
    /// Unique identifier of the merchant platform that we sent to you when you created the merchant platform.
    /// </summary>
    [JsonIgnore]
    public required string MerchantPlatformId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonIgnore]
    public required CreateProcessingAccount Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
