using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record ProcessingAccountContactsItem
{
    /// <summary>
    /// Unique identifier of the contact.
    /// </summary>
    [JsonPropertyName("contactId")]
    public int? ContactId { get; set; }

    /// <summary>
    /// Contact's first name.
    /// </summary>
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Contact's last name.
    /// </summary>
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    /// <summary>
    /// Object that contains HATEOAS links for the contact.
    /// </summary>
    [JsonPropertyName("link")]
    public ProcessingAccountContactsItemLink? Link { get; set; }

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
