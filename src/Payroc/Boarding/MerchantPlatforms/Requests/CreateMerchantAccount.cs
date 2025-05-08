using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

public record CreateMerchantAccount
{
    /// <summary>
    /// Unique identifier that you generate for each request. You must use the UUID v4 format for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonPropertyName("business")]
    public required Business Business { get; set; }

    /// <summary>
    /// Array of processingAccounts objects.
    /// </summary>
    [JsonPropertyName("processingAccounts")]
    public IEnumerable<CreateProcessingAccount> ProcessingAccounts { get; set; } =
        new List<CreateProcessingAccount>();

    /// <summary>
    /// Object that you can send to include custom data in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
