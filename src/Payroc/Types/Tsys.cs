using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Tsys
{
    /// <summary>
    /// Object that contains the configuration settings for the merchant.
    /// </summary>
    [JsonPropertyName("merchant")]
    public required TsysMerchant Merchant { get; set; }

    /// <summary>
    /// Object that contains the configuration settings for the terminal.
    /// </summary>
    [JsonPropertyName("terminal")]
    public required TsysTerminal Terminal { get; set; }

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
