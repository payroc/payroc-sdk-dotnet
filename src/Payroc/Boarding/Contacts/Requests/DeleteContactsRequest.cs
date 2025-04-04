using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.Contacts;

public record DeleteContactsRequest
{
    /// <summary>
    /// Unique identifier for the contact.
    /// </summary>
    [JsonIgnore]
    public required int ContactId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
