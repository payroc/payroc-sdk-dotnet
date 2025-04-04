using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record BaseUsAnnual
{
    /// <summary>
    /// Fee for the Platinum Security, this is returned in the lowest unit of currency. For example, cents.
    /// </summary>
    [JsonPropertyName("amount")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public int? Amount { get; set; }

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
