using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the terminal's host configuration.
/// </summary>
public record HostConfiguration
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier of the processing account.
    /// </summary>
    [JsonPropertyName("processingAccountId")]
    public string? ProcessingAccountId { get; set; }

    [JsonPropertyName("configuration")]
    public required HostConfigurationConfiguration Configuration { get; set; }

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
