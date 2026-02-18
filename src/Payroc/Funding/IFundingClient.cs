using Payroc.Funding.FundingAccounts;
using Payroc.Funding.FundingActivity;
using Payroc.Funding.FundingInstructions;
using Payroc.Funding.FundingRecipients;

namespace Payroc.Funding;

public partial interface IFundingClient
{
    public IFundingRecipientsClient FundingRecipients { get; }
    public IFundingAccountsClient FundingAccounts { get; }
    public IFundingInstructionsClient FundingInstructions { get; }
    public IFundingActivityClient FundingActivity { get; }
}
