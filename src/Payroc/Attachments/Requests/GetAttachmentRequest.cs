using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Attachments;

[Serializable]
public record GetAttachmentRequest
{
    /// <summary>
    /// Unique identifier of the attachment
    /// </summary>
    [JsonIgnore]
    public required string AttachmentId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
