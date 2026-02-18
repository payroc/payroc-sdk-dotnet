using Payroc.Core;
using Payroc.PaymentFeatures.Bank;
using Payroc.PaymentFeatures.Cards;

namespace Payroc.PaymentFeatures;

public partial class PaymentFeaturesClient : IPaymentFeaturesClient
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

    public ICardsClient Cards { get; }

    public IBankClient Bank { get; }
}
