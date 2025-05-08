using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the business.
/// </summary>
public record Business
{
    /// <summary>
    /// Legal name of the business.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Tax ID of the business.
    /// </summary>
    [JsonPropertyName("taxId")]
    public required string TaxId { get; set; }

    /// <summary>
    /// Type of organization.
    /// </summary>
    [JsonPropertyName("organizationType")]
    public required BusinessOrganizationType OrganizationType { get; set; }

    /// <summary>
    /// Two-digit country code for the country that the business operates in. The format follows the ISO-3166 standard.
    /// </summary>
    [JsonPropertyName("countryOfOperation")]
    public string? CountryOfOperation { get; set; }

    /// <summary>
    /// Array of address objects.
    /// </summary>
    [JsonPropertyName("addresses")]
    public IEnumerable<LegalAddress> Addresses { get; set; } = new List<LegalAddress>();

    /// <summary>
    /// An array of contactMethod objects. Email should always be provided.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

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
