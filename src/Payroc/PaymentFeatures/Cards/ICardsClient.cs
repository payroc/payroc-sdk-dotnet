using Payroc;

namespace Payroc.PaymentFeatures.Cards;

public partial interface ICardsClient
{
    /// <summary>
    /// Use this method to verify a customer’s card details.
    ///
    /// In the request, send the customer’s card details.
    ///
    /// In the response, our gateway indicates if the card details are valid and if you should use them in follow-on actions.
    /// </summary>
    WithRawResponseTask<CardVerificationResult> VerifyCardAsync(
        CardVerificationRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to view the balance of an Electronic Benefit Transfer (EBT) card.
    ///
    /// If the request is successful, our gateway returns the current balance of an EBT card.
    /// </summary>
    WithRawResponseTask<Balance> ViewEbtBalanceAsync(
        BalanceInquiry request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a debit card, a credit card, or an EBT card. If you apply surcharges to transactions, you can also check if the card supports surcharging.
    ///
    /// In the response, our gateway returns the following information about the card:
    ///
    /// - **Card details** - Information about the card, for example, the issuing bank and the masked card number.
    ///
    /// - **Surcharging information** - If you apply a surcharge to transactions, our gateway checks that the card supports surcharging and returns information about the surcharge. For more information about surcharging, go to [Credit card surcharging](https://docs.payroc.com/knowledge/card-payments/credit-card-surcharging).
    /// </summary>
    WithRawResponseTask<CardInfo> LookupBinAsync(
        BinLookup request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// &gt; **Important:** There are restrictions on which merchants can use this method. For more information, go to [Dynamic Currency Conversion](https://docs.payroc.com/knowledge/card-payments/dynamic-currency-conversion).
    ///
    /// Use this method to check if a card is eligible for Dynamic Currency Conversion (DCC) and to retrieve the conversion rate for a transaction amount. DCC provides a customer with the option to use their card's currency instead of the merchant's currency, for example, in Ireland, an American customer can pay in US dollars instead of Euros.
    ///
    /// The request includes the following:
    ///
    /// - **Payment method** - Card information, a secure token, or digital wallet.
    /// - **Transaction information** - Amount and currency of the transaction in the merchant's currency.
    ///
    /// If the card is eligible for DCC, our gateway returns the transaction amount in the card's currency and a dccOffer object that contains information about the conversion rate. The dccOffer object contains the following fields that you need when you [run a sale](https://docs.payroc.com/api/schema/card-payments/payments/create) or [unreferenced refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) with DCC:
    /// - fxAmount
    /// - fxCurrency
    /// - fxRate
    /// - markup
    /// - accepted
    /// - offerReference
    /// </summary>
    WithRawResponseTask<FxRate> RetrieveFxRatesAsync(
        FxRateInquiry request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
