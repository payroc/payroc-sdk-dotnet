using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

[Serializable]
public record GetFundingRecipientsRequest
{
    /// <summary>
    /// Unique identifier of the funding recipient.
    /// </summary>
    [JsonIgnore]
    public required int RecipientId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
