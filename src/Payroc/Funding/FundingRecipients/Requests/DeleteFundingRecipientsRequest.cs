using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

public record DeleteFundingRecipientsRequest
{
    /// <summary>
    /// Unique identifier of the funding recipient.
    /// </summary>
    [JsonIgnore]
    public required int RecipientId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
