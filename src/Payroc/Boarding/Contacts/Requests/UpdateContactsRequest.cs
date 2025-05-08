using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.Contacts;

public record UpdateContactsRequest
{
    /// <summary>
    /// Unique identifier for the contact.
    /// </summary>
    [JsonIgnore]
    public required int ContactId { get; set; }

    [JsonIgnore]
    public required Contact Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
