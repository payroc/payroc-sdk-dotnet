using Payroc.Boarding.Contacts;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.Owners;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
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
        Contacts = new ContactsClient(_client);
    }

    public OwnersClient Owners { get; }

    public PricingIntentsClient PricingIntents { get; }

    public MerchantPlatformsClient MerchantPlatforms { get; }

    public ProcessingAccountsClient ProcessingAccounts { get; }

    public ContactsClient Contacts { get; }
}
