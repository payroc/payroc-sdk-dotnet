using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the unencrypted card details.
/// </summary>
[Serializable]
public record RawCardDetails
{
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
    /// Cardholder's signature. For more information about how to format the signature, go to [How to send a signature to our gateway](/knowledge/basic-concepts/signature-capture).
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public string? CardholderSignature { get; set; }

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
