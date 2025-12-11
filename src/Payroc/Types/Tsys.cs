using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record Tsys : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
