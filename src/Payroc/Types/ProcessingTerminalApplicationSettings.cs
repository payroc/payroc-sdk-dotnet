using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the application settings for the solution.
/// </summary>
[Serializable]
public record ProcessingTerminalApplicationSettings : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Indicates if the terminal should prompt the clerk to provide an invoice number with a sale.
    /// </summary>
    [JsonPropertyName("invoiceNumberPrompt")]
    public bool? InvoiceNumberPrompt { get; set; }

    /// <summary>
    /// Indicates if the terminal should prompt the clerk, for example, if the terminal should prompt when the clerk needs to enter an amount on the terminal.
    /// </summary>
    [JsonPropertyName("clerkPrompt")]
    public bool? ClerkPrompt { get; set; }

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
