using Payroc.Boarding.Contacts;
using Payroc.Boarding.MerchantPlatforms;
using Payroc.Boarding.Owners;
using Payroc.Boarding.PricingIntents;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Boarding.ProcessingTerminals;
using Payroc.Boarding.TerminalOrders;

namespace Payroc.Boarding;

public partial interface IBoardingClient
{
    public IOwnersClient Owners { get; }
    public IPricingIntentsClient PricingIntents { get; }
    public IMerchantPlatformsClient MerchantPlatforms { get; }
    public IProcessingAccountsClient ProcessingAccounts { get; }
    public IProcessingTerminalsClient ProcessingTerminals { get; }
    public IContactsClient Contacts { get; }
    public ITerminalOrdersClient TerminalOrders { get; }
}
