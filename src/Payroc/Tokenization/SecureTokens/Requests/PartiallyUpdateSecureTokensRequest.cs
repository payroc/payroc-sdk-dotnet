using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Tokenization.SecureTokens;

[Serializable]
public record PartiallyUpdateSecureTokensRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that the merchant assigned to the secure token.
    /// </summary>
    [JsonIgnore]
    public required string SecureTokenId { get; set; }

    /// <summary>
    /// Unique identifier that you generate for each request. You must use the [UUID v4 format](https://www.rfc-editor.org/rfc/rfc4122) for the identifier. For more information about the idempotency key, go to [Idempotency](https://docs.payroc.com/api/idempotency).
    /// </summary>
    [JsonIgnore]
    public required string IdempotencyKey { get; set; }

    [JsonIgnore]
    public IEnumerable<PatchDocument> Body { get; set; } = new List<PatchDocument>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
