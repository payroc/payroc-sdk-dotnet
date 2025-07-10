using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the business.
/// </summary>
[Serializable]
public record Business : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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
    /// Two-digit country code for the country that the business operates in. The format follows the [ISO-3166](https://www.iso.org/iso-3166-country-codes.html) standard.
    /// </summary>
    [JsonPropertyName("countryOfOperation")]
    public string? CountryOfOperation { get; set; }

    /// <summary>
    /// Type of address.
    /// </summary>
    [JsonPropertyName("addresses")]
    public IEnumerable<LegalAddress> Addresses { get; set; } = new List<LegalAddress>();

    /// <summary>
    /// Array of contactMethod objects. One contact method must be an email address.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
