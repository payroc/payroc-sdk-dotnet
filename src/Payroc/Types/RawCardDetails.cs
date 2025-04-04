using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the unencrypted card details.
/// </summary>
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
    /// Cardholder's signature in the format described in the [Special Fields and Parameters](https://worldnet.payroc.com/selfcare:api_specification:special_fields_and_parameters#the_signature_field_format) section.
    /// </summary>
    [JsonPropertyName("cardholderSignature")]
    public string? CardholderSignature { get; set; }

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
