using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.PricingIntents;

public record UpdatePricingIntentsRequest
{
    /// <summary>
    /// Unique identifier of the pricing intent.
    /// </summary>
    [JsonIgnore]
    public required string PricingIntentId { get; set; }

    [JsonIgnore]
    public required PricingIntent50 Body { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
