using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.Owners;

[Serializable]
public record RetrieveOwnersRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the owner.
    /// </summary>
    [JsonIgnore]
    public required int OwnerId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
