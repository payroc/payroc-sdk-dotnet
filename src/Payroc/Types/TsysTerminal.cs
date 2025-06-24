using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

/// <summary>
/// Object that contains the configuration settings for the terminal.
/// </summary>
[Serializable]
public record TsysTerminal
{
    /// <summary>
    /// Unique identifier that the host processor assigned to the terminal.
    /// </summary>
    [JsonPropertyName("terminalId")]
    public required string TerminalId { get; set; }

    /// <summary>
    /// Unique identifier of the terminal at the merchant's site.
    /// </summary>
    [JsonPropertyName("terminalNumber")]
    public required string TerminalNumber { get; set; }

    /// <summary>
    /// Authenticates the terminal's identity with the host processor.
    /// </summary>
    [JsonPropertyName("authenticationCode")]
    public string? AuthenticationCode { get; set; }

    /// <summary>
    /// Indicates the direct debit networks and EBT networks that the terminal can use.
    /// </summary>
    [JsonPropertyName("sharingGroups")]
    public string? SharingGroups { get; set; }

    /// <summary>
    /// Indicates if the terminal can run Mail Order/Telephone Order (MOTO) transactions.
    /// </summary>
    [JsonPropertyName("motoAllowed")]
    public bool? MotoAllowed { get; set; }

    /// <summary>
    /// Indicates if the terminal can run e-Commerce transactions.
    /// </summary>
    [JsonPropertyName("internetAllowed")]
    public bool? InternetAllowed { get; set; }

    /// <summary>
    /// Indicates if the terminal can run card present transactions.
    /// </summary>
    [JsonPropertyName("cardPresentAllowed")]
    public bool? CardPresentAllowed { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    /// <remarks>
    /// [EXPERIMENTAL] This API is experimental and may change in future releases.
    /// </remarks>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
