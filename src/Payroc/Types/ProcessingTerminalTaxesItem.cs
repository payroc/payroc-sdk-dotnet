using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains a tax rate with a short description of the tax rate.
/// </summary>
[Serializable]
public record ProcessingTerminalTaxesItem : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Rate of tax that the terminal applies to each transaction.
    /// </summary>
    [JsonPropertyName("taxRate")]
    public required float TaxRate { get; set; }

    /// <summary>
    /// Short description of the tax rate, for example, "Sales Tax".
    /// </summary>
    [JsonPropertyName("taxLabel")]
    public required string TaxLabel { get; set; }

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
