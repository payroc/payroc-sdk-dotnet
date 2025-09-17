using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.SecureTokens;

[Serializable]
public record ListSecureTokensRequest
{
    /// <summary>
    /// Unique identifier that we assigned to the terminal.
    /// </summary>
    [JsonIgnore]
    public required string ProcessingTerminalId { get; set; }

    /// <summary>
    /// Unique identifier that the merchant assigned to the secure token.
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
    /// Return the previous page of results before the value that you specify.
    ///
    /// You can’t send a before parameter in the same request as an after parameter.
    /// </summary>
    [JsonIgnore]
    public string? Before { get; set; }

    /// <summary>
    /// Return the next page of results after the value that you specify.
    ///
    /// You can’t send an after parameter in the same request as a before parameter.
    /// </summary>
    [JsonIgnore]
    public string? After { get; set; }

    /// <summary>
    /// Limit the maximum number of results that we return for each page.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
