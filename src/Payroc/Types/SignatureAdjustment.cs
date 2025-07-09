using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the signature for the transaction.
/// **Note:** If the merchant previously added a signature to the transaction, they can’t adjust or delete the signature.
/// </summary>
[Serializable]
public record SignatureAdjustment : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Cardholder’s signature. For more information about the format of the signature, see Special Fields and Parameters.
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public required string CardholderSignature { get; set; }

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
