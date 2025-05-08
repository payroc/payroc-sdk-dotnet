using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.SecureTokens;

public record ListSecureTokensRequest
{
    /// <summary>
    /// Unique identifier that our gateway assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Filter by the unique secure token.
    /// </summary>
    [JsonIgnore]
    public string? SecureTokenId { get; set; }

    /// <summary>
    /// Filter by the customer's name.
    /// </summary>
    [JsonIgnore]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Filter by the customer's phone number.
    /// </summary>
    [JsonIgnore]
    public string? Phone { get; set; }

    /// <summary>
    /// Filter by the customer's email address.
    /// </summary>
    [JsonIgnore]
    public string? Email { get; set; }

    /// <summary>
    /// Filter by the token that the merchant used in a transaction to represent the customer's payment details.
    /// </summary>
    [JsonIgnore]
    public string? Token { get; set; }

    /// <summary>
    /// Filter by the first six digits of the card number.
    /// </summary>
    [JsonIgnore]
    public string? First6 { get; set; }

    /// <summary>
    /// Filter by the last four digits of the card or account number.
    /// </summary>
    [JsonIgnore]
    public string? Last4 { get; set; }

    /// <summary>
    /// Points to the resource identifier that you want to receive your results before. Typically, this is the first resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Points to the resource identifier that you want to receive your results after. Typically, this is the last resource on the previous page.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// States the total amount of results the response is limited to.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
