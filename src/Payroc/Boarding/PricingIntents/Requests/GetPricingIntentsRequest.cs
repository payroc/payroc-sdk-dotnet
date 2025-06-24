using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.PricingIntents;

[Serializable]
public record GetPricingIntentsRequest
{
    /// <summary>
    /// Unique identifier of the pricing intent.
    /// </summary>
    [JsonIgnore]
    public required string PricingIntentId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
