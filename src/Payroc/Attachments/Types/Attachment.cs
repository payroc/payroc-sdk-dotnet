using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Attachments;

[Serializable]
public record Attachment : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that our gateway assigned to the attachment.
    /// </summary>
    [JsonPropertyName("attachmentId")]
    public required string AttachmentId { get; set; }

    /// <summary>
    /// Type of attachment.
    /// </summary>
    [JsonPropertyName("type")]
    public required AttachmentType Type { get; set; }

    /// <summary>
    /// Upload status of the attachment. The value is one of the following:
    /// - `pending` - We have not yet uploaded the attachment.
    /// - `accepted` - We have uploaded the attachment.
    /// - `rejected` - We rejected the attachment.
    /// </summary>
    [JsonPropertyName("uploadStatus")]
    public required AttachmentUploadStatus UploadStatus { get; set; }

    /// <summary>
    /// Name of the file.
    /// </summary>
    [JsonPropertyName("fileName")]
    public required string FileName { get; set; }

    /// <summary>
    /// Content type of the file.
    /// </summary>
    [JsonPropertyName("contentType")]
    public required string ContentType { get; set; }

    /// <summary>
    /// Short description of the attachment.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Object that contains information about the entity that the attachment is linked to.
    /// </summary>
    [JsonPropertyName("entity")]
    public required AttachmentEntity Entity { get; set; }

    /// <summary>
    /// Date and time that we received your request to upload the attachment.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public required DateTime CreatedDate { get; set; }

    /// <summary>
    /// Date and time the attachment was last modified.
    /// </summary>
    [JsonPropertyName("lastModifiedDate")]
    public required DateTime LastModifiedDate { get; set; }

    /// <summary>
    /// Object that you can send to include custom metadata in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

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
