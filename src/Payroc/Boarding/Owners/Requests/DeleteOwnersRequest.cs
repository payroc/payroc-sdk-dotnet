using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.Owners;

public record DeleteOwnersRequest
{
    /// <summary>
    /// Unique identifier for the owner.
    /// </summary>
    [JsonIgnore]
    public required int OwnerId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
