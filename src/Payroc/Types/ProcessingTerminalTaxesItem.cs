using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains a tax rate with a short description of the tax rate.
/// </summary>
[Serializable]
public record ProcessingTerminalTaxesItem
{
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
