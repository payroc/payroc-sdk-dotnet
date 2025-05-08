using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about encrypted PIN details.
/// </summary>
public record DukptPinDetails
{
    /// <summary>
    /// Encrypted PIN.
    /// **Note:** PIN is encrypted using the DUKPT scheme.
    /// </summary>
    [JsonPropertyName("pin")]
    public required string Pin { get; set; }

    /// <summary>
    /// Key serial number.
    /// </summary>
    [JsonPropertyName("pinKsn")]
    public required string PinKsn { get; set; }

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
