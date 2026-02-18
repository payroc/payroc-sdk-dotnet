using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[Serializable]
public record Contact : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier of the contact.
    /// </summary>
    [JsonAccess(JsonAccessType.ReadOnly)]
    [JsonPropertyName("contactId")]
    public int? ContactId { get; set; }

    /// <summary>
    /// Type of contact.
    /// </summary>
    [JsonPropertyName("type")]
    public required ContactType Type { get; set; }

    /// <summary>
    /// Contact's first name.
    /// </summary>
    [JsonPropertyName("firstName")]
    public required string FirstName { get; set; }

    /// <summary>
    /// Contact's middle name.
    /// </summary>
    [JsonPropertyName("middleName")]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Contact's last name.
    /// </summary>
    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    /// <summary>
    /// Array of identifier objects.
    /// </summary>
    [JsonPropertyName("identifiers")]
    public IEnumerable<Identifier> Identifiers { get; set; } = new List<Identifier>();

    /// <summary>
    /// Array of polymorphic objects, which contain contact information.
    ///
    /// **Note:** If you are adding information about an owner, you must provide at least an email address. If you are adding information about a contact, you must provide at least a contact number.
    ///
    /// The value of the type parameter determines which variant you should use:
    /// -	`email` - Email address
    /// -	`phone` - Phone number
    /// -	`mobile` - Mobile number
    /// -	`fax` - Fax number
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
