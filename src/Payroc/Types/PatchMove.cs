using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// A Patch Move Operation.
/// </summary>
[Serializable]
public record PatchMove
{
    /// <summary>
    /// Location of the value that you want to move.
    /// The format for this value is JSON Pointer.
    /// </summary>
    [JsonPropertyName("from")]
    public required string From { get; set; }

    /// <summary>
    /// Location where you want to move the value to.
    /// The format for this value is JSON Pointer.
    /// </summary>
    [JsonPropertyName("path")]
    public required string Path { get; set; }

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
