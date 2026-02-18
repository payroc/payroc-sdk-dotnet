using Payroc.Boarding.Contacts;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.Owners;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Boarding.ProcessingTerminals;
using Payroc.Boarding.TerminalOrders;
using Payroc.Core;

namespace Payroc.Boarding;

public partial class BoardingClient : IBoardingClient
{
    private RawClient _client;

    internal BoardingClient(RawClient client)
    {
        try
        {
            _client = client;
            Owners = new OwnersClient(_client);
            PricingIntents = new PricingIntentsClient(_client);
            MerchantPlatforms = new MerchantPlatformsClient(_client);
            ProcessingAccounts = new ProcessingAccountsClient(_client);
            ProcessingTerminals = new ProcessingTerminalsClient(_client);
            Contacts = new ContactsClient(_client);
            TerminalOrders = new TerminalOrdersClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public IOwnersClient Owners { get; }

    public IPricingIntentsClient PricingIntents { get; }

    public IMerchantPlatformsClient MerchantPlatforms { get; }

    public IProcessingAccountsClient ProcessingAccounts { get; }

    public IProcessingTerminalsClient ProcessingTerminals { get; }

    public IContactsClient Contacts { get; }

    public ITerminalOrdersClient TerminalOrders { get; }
}
