using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the address.
/// </summary>
public record Address
{
    /// <summary>
    /// Address Line.
    /// </summary>
    [JsonPropertyName("address1")]
    public required string Address1 { get; set; }

    /// <summary>
    /// Address Line.
    /// </summary>
    [JsonPropertyName("address2")]
    public string? Address2 { get; set; }

    /// <summary>
    /// Address Line.
    /// </summary>
    [JsonPropertyName("address3")]
    public string? Address3 { get; set; }

    /// <summary>
    /// City.
    /// </summary>
    [JsonPropertyName("city")]
    public required string City { get; set; }

    /// <summary>
    /// Name or postal abbreviation of the state.
    /// </summary>
    [JsonPropertyName("state")]
    public required string State { get; set; }

    /// <summary>
    /// Two letter ISO 3166-1 country code.
    /// </summary>
    [JsonPropertyName("country")]
    public required string Country { get; set; }

    /// <summary>
    /// Zip Code or Postcode.
    /// </summary>
    [JsonPropertyName("postalCode")]
    public required string PostalCode { get; set; }

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
