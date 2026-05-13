using Payroc;
using Payroc.Boarding.MerchantPlatforms;

namespace Payroc.TestCommon.HelperMethods.Boarding;

internal static class MerchantAccountHelper
{
    // Pins cardsAccepted and AmericanExpressDirect.Enabled to values the UAT API accepts.
    // The TestData refresh can remove amexoptblue or set AmericanExpressDirect.Enabled=true,
    // both of which cause BadRequestError. Call this after Data.Get<CreateMerchantAccount>.
    internal static void ApplyStableCardAcceptance(CreateMerchantAccount request)
    {
        var cardAcceptance = request.ProcessingAccounts.First().Processing?.CardAcceptance;
        if (cardAcceptance == null) return;

        cardAcceptance.CardsAccepted =
        [
            ProcessingCardAcceptanceCardsAcceptedItem.Visa,
            ProcessingCardAcceptanceCardsAcceptedItem.Mastercard,
            ProcessingCardAcceptanceCardsAcceptedItem.AmexOptBlue,
        ];
        if (cardAcceptance.SpecialityCards?.AmericanExpressDirect != null)
            cardAcceptance.SpecialityCards.AmericanExpressDirect.Enabled = false;
    }
}
