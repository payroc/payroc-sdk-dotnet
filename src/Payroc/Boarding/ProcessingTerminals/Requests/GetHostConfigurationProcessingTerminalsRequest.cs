using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingTerminals;

[Serializable]
public record GetHostConfigurationProcessingTerminalsRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
