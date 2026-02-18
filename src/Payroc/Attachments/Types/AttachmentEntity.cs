using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.Attachments;

/// <summary>
/// Object that contains information about the entity that the attachment is linked to.
/// </summary>
[Serializable]
public record AttachmentEntity : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Type of entity that the attachment is linked to.
    /// </summary>
    [JsonPropertyName("type")]
    public required AttachmentEntityType Type { get; set; }

    /// <summary>
    /// Unique identifier of the entity that the attachment is linked to.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

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
