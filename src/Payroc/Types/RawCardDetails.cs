using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the unencrypted card details.
/// </summary>
[Serializable]
public record RawCardDetails : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// If an offline transaction is not approved using the initial entry method, reprocess the transaction using a downgraded entry method.
    /// For example, an Integrated Circuit Card (ICC) transaction can be downgraded to a swiped transaction or to a keyed transaction.
    /// </summary>
    [JsonPropertyName("downgradeTo")]
    public RawCardDetailsDowngradeTo? DowngradeTo { get; set; }

    [JsonPropertyName("device")]
    public required Device Device { get; set; }

    /// <summary>
    /// Unencrypted data from the POS terminal.
    /// </summary>
    [JsonPropertyName("rawData")]
    public required string RawData { get; set; }

    /// <summary>
    /// Cardholder's signature. For more information about how to format the signature, go to [How to send a signature to our gateway](https://docs.payroc.com/knowledge/basic-concepts/signature-capture).
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public string? CardholderSignature { get; set; }

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
