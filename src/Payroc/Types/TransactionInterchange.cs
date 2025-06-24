using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the interchange fees for the transaction.
/// </summary>
[Serializable]
public record TransactionInterchange
{
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
