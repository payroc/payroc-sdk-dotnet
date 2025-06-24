using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.Owners;

[Serializable]
public record UpdateOwnersRequest
{
    /// <summary>
    /// Unique identifier for the owner.
    /// </summary>
    [JsonIgnore]
    public required int OwnerId { get; set; }

    [JsonIgnore]
    public required Owner Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
