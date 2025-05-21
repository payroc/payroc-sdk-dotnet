using Payroc.Core;
using System.Text.Json.Serialization;

namespace Payroc;

public record SignatureWrapper
{
    /// <summary>
    /// Type of signature.
    /// </summary>
    [JsonPropertyName("type")]
    public required CreateProcessingAccountSignature Type { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
