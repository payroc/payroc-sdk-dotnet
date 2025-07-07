using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the customer’s payment card.
/// </summary>
[Serializable]
public record CardPayload
{
    /// <summary>
    /// Indicates the customer’s account type.
    /// **Note:** Credit card transactions don't require **accountType**.
    /// </summary>
    [JsonPropertyName("accountType")]
    public CardPayloadAccountType? AccountType { get; set; }

    /// <summary>
    /// Object that contains the details of the payment card.
    /// </summary>
    [JsonPropertyName("cardDetails")]
    public required CardPayloadCardDetails CardDetails { get; set; }

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
