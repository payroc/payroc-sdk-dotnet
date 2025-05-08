using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.SecureTokens;

public record DeleteSecureTokensRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter by the unique secure token.
    /// </summary>
    [JsonIgnore]
    public required string SecureTokenId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
