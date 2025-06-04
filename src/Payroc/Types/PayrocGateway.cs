using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the gateway settings for the solution.
/// </summary>
public record PayrocGateway
{
    /// <summary>
    /// Name of the gateway that processes the transactions.
    /// </summary>
    [JsonPropertyName("gateway")]
    public string Gateway { get; set; } = "payroc";

    /// <summary>
    /// Unique identifier of the gateway terminal template.
    /// </summary>
    [JsonPropertyName("terminalTemplateId")]
    public required string TerminalTemplateId { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
