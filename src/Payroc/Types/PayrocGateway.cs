using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the gateway settings for the solution.
/// </summary>
[Serializable]
public record PayrocGateway : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Name of the gateway that processes the transactions.
    /// </summary>
    [JsonPropertyName("gateway")]
    public required PayrocGatewayGateway Gateway { get; set; }

    /// <summary>
    /// Unique identifier of the gateway terminal template.
    /// </summary>
    [JsonPropertyName("terminalTemplateId")]
    public required string TerminalTemplateId { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
