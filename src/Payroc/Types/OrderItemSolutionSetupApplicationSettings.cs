using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the application settings for the solution.
/// </summary>
[Serializable]
public record OrderItemSolutionSetupApplicationSettings
{
    /// <summary>
    /// Indicates if the terminal should prompt the clerk, for example, if the terminal should prompt when the clerk needs to enter an amount on the terminal.
    /// </summary>
    [JsonPropertyName("clerkPrompt")]
    public bool? ClerkPrompt { get; set; }

    /// <summary>
    /// Object that contains the password settings when running specific transaction types.
    /// </summary>
    [JsonPropertyName("security")]
    public OrderItemSolutionSetupApplicationSettingsSecurity? Security { get; set; }

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
