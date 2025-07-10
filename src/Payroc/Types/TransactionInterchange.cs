using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the interchange fees for the transaction.
/// </summary>
[Serializable]
public record TransactionInterchange : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Interchange basis points that we apply to the transaction.
    /// </summary>
    [JsonPropertyName("basisPoint")]
    public int? BasisPoint { get; set; }

    /// <summary>
    /// Interchange fee for the transaction. We return the value in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("transactionFee")]
    public int? TransactionFee { get; set; }

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
