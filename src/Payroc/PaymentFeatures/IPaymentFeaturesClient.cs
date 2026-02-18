using Payroc.PaymentFeatures.Bank;
using Payroc.PaymentFeatures.Cards;

namespace Payroc.PaymentFeatures;

public partial interface IPaymentFeaturesClient
{
    public ICardsClient Cards { get; }
    public IBankClient Bank { get; }
}
