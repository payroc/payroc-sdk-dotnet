using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains information about the status of the instruction
/// </summary>
[Serializable]
public record DeviceInstruction : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates the current status of the instruction.
    /// - `canceled` – The instruction was canceled before it was completed.
    /// - `completed` – The instruction has completed. Use the link object to check the resource.
    /// - `failure` – The instruction failed. Check the errorMessage field for more information.
    /// - `inProgress` – The instruction is currently in progress.
    /// </summary>
    [JsonPropertyName("status")]
    public DeviceInstructionStatus? Status { get; set; }

    /// <summary>
    /// Description of the error that caused the instruction to fail.
    ///
    /// **Note:** We return this field only if the status is `failure`.
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

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
