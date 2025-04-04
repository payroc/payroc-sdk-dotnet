using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about dual pricing.
/// </summary>
public record DualPricing
{
    /// <summary>
    /// Indicates if the merchant offers dual pricing to the customer.
    /// </summary>
    [JsonPropertyName("offered")]
    public required bool Offered { get; set; }

    /// <summary>
    /// Object that contains information about the choice rate.
    /// **Note:** For requests, if the value for **offered** is `true`, you must send this object in the request.
    /// </summary>
    [JsonPropertyName("choiceRate")]
    public ChoiceRate? ChoiceRate { get; set; }

    /// <summary>
    /// Payment method that the merchant presented to the customer as an alternative to their chosen method.
    /// **Note:** For requests, if the value for **offered** is `true`, you must send a value for **alternativeTender** in the request.
    /// </summary>
    [JsonPropertyName("alternativeTender")]
    public DualPricingAlternativeTender? AlternativeTender { get; set; }

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
