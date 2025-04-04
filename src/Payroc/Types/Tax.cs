using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Tax
{
    /// <summary>
    /// Name of the tax.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Tax percentage for the transaction.
    /// </summary>
    [JsonPropertyName("rate")]
    public required double Rate { get; set; }

    /// <summary>
    /// Amount of tax that was applied to the transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public long? Amount { get; set; }

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
