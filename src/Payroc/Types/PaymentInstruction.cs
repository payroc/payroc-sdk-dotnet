using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

public record PaymentInstruction
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the instruction.
    /// </summary>
    [JsonPropertyName("paymentInstructionId")]
    public string? PaymentInstructionId { get; set; }

    /// <summary>
    /// Indicates the current status of the instruction.
    /// - `canceled` – The instruction was canceled before it was completed.
    /// - `completed` – The instruction has completed. Use the link to check the resource.
    /// - `failure` – The instruction failed. Check the error message for more information.
    /// - `inProgress` – The instruction is currently in progress.
    /// </summary>
    [JsonPropertyName("status")]
    public DeviceInstructionStatus? Status { get; set; }

    /// <summary>
    /// Description of the error that caused the instruction to fail.
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("link")]
    public Link? Link { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
