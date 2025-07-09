using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains details about EBT transactions.
/// </summary>
[Serializable]
public record EbtEnabled : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the terminal accepts Electronic Benefit Transfer (EBT) transactions.
    /// </summary>
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }

    /// <summary>
    /// Indicates the type of EBT that the terminal supports.
    /// </summary>
    [JsonPropertyName("ebtType")]
    public required EbtEnabledEbtType EbtType { get; set; }

    /// <summary>
    /// Food and Nutritional Service (FNS) number that the government assigns to the merchant to allow them to accept Supplemental Nutrition Assistance Program (SNAP) transactions.
    /// </summary>
    [JsonPropertyName("fnsNumber")]
    public string? FnsNumber { get; set; }

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
