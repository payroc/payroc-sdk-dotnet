using Payroc;
using Payroc.Core;

namespace Payroc.PaymentLinks.SharingEvents;

public partial interface ISharingEventsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of sharing events for a payment link. A sharing event occurs when a merchant shares a payment link with a customer.
    ///
    /// To list the sharing events for a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payment-links/list) method to search for the payment link.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for links sent to a specific customer.
    ///
    /// Our gateway returns the following information for each sharing event in the list:
    /// - Customer that the merchant sent the link to.
    /// - Date that the merchant sent the link.
    /// </summary>
    Task<PayrocPager<PaymentLinkEmailShareEvent>> ListAsync(
        ListSharingEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to email a payment link to a customer.
    ///
    /// To email a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payment-links/list) method to search for the payment link.
    ///
    /// In the request, you must provide the recipient's name and email address.
    ///
    /// In the response, our gateway returns a sharingEventId, which you can use to [List Payment Link Sharing Events](https://docs.payroc.com/api/schema/payment-links/sharing-events/list).
    /// </summary>
    WithRawResponseTask<PaymentLinkEmailShareEvent> ShareAsync(
        ShareSharingEventsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
