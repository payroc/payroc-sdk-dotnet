using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the gateway fees.
/// </summary>
public record GatewayUsFees
{
    /// <summary>
    /// Monthly fee for the gateway. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("monthly")]
    public required int Monthly { get; set; }

    /// <summary>
    /// Fee for setting up your account with the gateway. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("setup")]
    public required int Setup { get; set; }

    /// <summary>
    /// Fee for each transaction. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("perTransaction")]
    public required int PerTransaction { get; set; }

    /// <summary>
    /// Monthly fee for each device. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("perDeviceMonthly")]
    public required int PerDeviceMonthly { get; set; }

    /// <summary>
    /// Monthly fee for additional service. The value is in the currency's lowest denomination, for example, cents.
    /// </summary>
    [JsonPropertyName("additionalServiceMonthly")]
    public required int AdditionalServiceMonthly { get; set; }

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
