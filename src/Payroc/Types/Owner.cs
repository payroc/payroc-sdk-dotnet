using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record Owner
{
    /// <summary>
    /// Unique identifier of the owner.
    /// </summary>
    [JsonPropertyName("ownerId")]
    [JsonAccess(JsonAccessType.ReadOnly)]
    public int? OwnerId { get; set; }

    /// <summary>
    /// Owner's first name.
    /// </summary>
    [JsonPropertyName("firstName")]
    public required string FirstName { get; set; }

    /// <summary>
    /// Owner's middle name.
    /// </summary>
    [JsonPropertyName("middleName")]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Owner's last name.
    /// </summary>
    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    /// <summary>
    /// Owner's date of birth. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("dateOfBirth")]
    public required string DateOfBirth { get; set; }

    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    /// <summary>
    /// Array of identifier objects.
    /// </summary>
    [JsonPropertyName("identifiers")]
    public IEnumerable<Identifier> Identifiers { get; set; } = new List<Identifier>();

    /// <summary>
    /// Array of contactMethod objects.
    /// **Note**: If you are adding information about an owner, you must provide at least an email address. If you are adding information about a contact, you must provide at least a contact number.
    /// </summary>
    [JsonPropertyName("contactMethods")]
    public IEnumerable<ContactMethod> ContactMethods { get; set; } = new List<ContactMethod>();

    /// <summary>
    /// Object that contains information about the owner's relationship to the business.
    /// </summary>
    [JsonPropertyName("relationship")]
    public required OwnerRelationship Relationship { get; set; }

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
