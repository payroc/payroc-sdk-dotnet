using Payroc.Boarding.Contacts;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.Owners;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Boarding.ProcessingTerminals;
using Payroc.Boarding.TerminalOrders;
using Payroc.Core;

namespace Payroc.Boarding;

public partial class BoardingClient
{
    private RawClient _client;

    internal BoardingClient(RawClient client)
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

    public OwnersClient Owners { get; }

    public PricingIntentsClient PricingIntents { get; }

    public MerchantPlatformsClient MerchantPlatforms { get; }

    public ProcessingAccountsClient ProcessingAccounts { get; }

    public ProcessingTerminalsClient ProcessingTerminals { get; }

    public ContactsClient Contacts { get; }

    public TerminalOrdersClient TerminalOrders { get; }
}
