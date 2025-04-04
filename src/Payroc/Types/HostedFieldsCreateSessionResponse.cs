using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record HostedFieldsCreateSessionResponse
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Token that our gateway assigned to the Hosted Fields session.
    ///
    /// Include this session token in the config file for Hosted Fields.
    ///
    /// The session token expires after 10 minutes.
    /// </summary>
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    /// <summary>
    /// Date and time that the token expires. We return this value in the ISO 8601 format.
    /// </summary>
    [JsonPropertyName("expiresAt")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public required DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
