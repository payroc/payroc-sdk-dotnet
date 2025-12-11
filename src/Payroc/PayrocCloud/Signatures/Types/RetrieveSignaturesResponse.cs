using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc;
using Payroc.Core;

namespace Payroc.PayrocCloud.Signatures;

[Serializable]
public record RetrieveSignaturesResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier that we assigned to the signature.
    /// </summary>
    [JsonPropertyName("signatureId")]
    public string? SignatureId { get; set; }

    /// <summary>
    /// Unique identifier of the terminal that the signature is linked to.
    /// </summary>
    [JsonPropertyName("processingTerminalId")]
    public string? ProcessingTerminalId { get; set; }

    /// <summary>
    /// Date that the device captured the signature. The format of this value is **YYYY-MM-DD**.
    /// </summary>
    [JsonPropertyName("createdOn")]
    public DateOnly? CreatedOn { get; set; }

    /// <summary>
    /// MIME type that indicates the format of the image file.
    /// </summary>
    [JsonPropertyName("contentType")]
    public string? ContentType { get; set; }

    /// <summary>
    /// Image data for the signature. Our gateway returns the signature as a Base64-encoded value.
    /// </summary>
    [JsonPropertyName("signature")]
    public string? Signature { get; set; }

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
