using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PayrocCloud.Signatures;

[Serializable]
public record RetrieveSignaturesRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the signature.
    /// </summary>
    [JsonIgnore]
    public required string SignatureId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
