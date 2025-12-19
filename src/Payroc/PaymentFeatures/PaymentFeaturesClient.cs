using Payroc.Core;
using Payroc.PaymentFeatures.Bank;
using Payroc.PaymentFeatures.Cards;

namespace Payroc.PaymentFeatures;

public partial class PaymentFeaturesClient
{
    private RawClient _client;

    internal PaymentFeaturesClient(RawClient client)
    {
        try
        {
            _client = client;
            Cards = new CardsClient(_client);
            Bank = new BankClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public CardsClient Cards { get; }

    public BankClient Bank { get; }
}
