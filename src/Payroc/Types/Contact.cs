using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Contact
{
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
    /// Array of contactMethod objects.
    /// **Note:** If you are adding information about an owner, you must provide at least an email address. If you are adding information about a contact, you must provide at least a contact number.
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
