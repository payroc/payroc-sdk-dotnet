using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the customer’s payment card.
/// </summary>
[Serializable]
public record CardPayload : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates the customer’s account type.
    ///
    /// **Note:** Send a value for accountType only for bank account details.
    /// </summary>
    [JsonPropertyName("accountType")]
    public CardPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Object that contains the details of the payment card.
    /// </summary>
    [JsonPropertyName("cardDetails")]
    public required CardPayloadCardDetails CardDetails { get; set; }

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
