using Payroc.Core;
using Payroc.Funding.FundingAccounts;
using Payroc.Funding.FundingActivity;
using Payroc.Funding.FundingInstructions;
using Payroc.Funding.FundingRecipients;

namespace Payroc.Funding;

public partial class FundingClient : IFundingClient
{
    private RawClient _client;

    internal FundingClient(RawClient client)
    {
        try
        {
            _client = client;
            FundingRecipients = new FundingRecipientsClient(_client);
            FundingAccounts = new FundingAccountsClient(_client);
            FundingInstructions = new FundingInstructionsClient(_client);
            FundingActivity = new FundingActivityClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public IFundingRecipientsClient FundingRecipients { get; }

    public IFundingAccountsClient FundingAccounts { get; }

    public IFundingInstructionsClient FundingInstructions { get; }

    public IFundingActivityClient FundingActivity { get; }
}
