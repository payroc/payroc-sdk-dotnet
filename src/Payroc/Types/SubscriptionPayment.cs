using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record SubscriptionPayment
{
    /// <summary>
    /// Unique identifier that the merchant assigned to the subscription.
    /// </summary>
    [JsonPropertyName("subscriptionId")]
    public required string SubscriptionId { get; set; }

    /// <summary>
    /// Unique identifier of the terminal that the subscription is linked to.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public required string ProcessingTerminalId { get; set; }

    [JsonPropertyName("payment")]
    public required PaymentSummary Payment { get; set; }

    [JsonPropertyName("secureToken")]
    public required SecureTokenSummary SecureToken { get; set; }

    [JsonPropertyName("currentState")]
    public required SubscriptionState CurrentState { get; set; }

    /// <summary>
    /// Array of customField objects.
    /// </summary>
    [JsonPropertyName("customFields")]
    public IEnumerable<CustomField>? CustomFields { get; set; }

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
