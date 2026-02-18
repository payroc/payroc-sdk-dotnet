using Payroc;
using Payroc.Core;
using Payroc.PaymentLinks.SharingEvents;

namespace Payroc.PaymentLinks;

public partial interface IPaymentLinksClient
{
    public ISharingEventsClient SharingEvents { get; }

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payment links linked to a processing terminal.
    ///
    /// **Note:** If you want to view the details of a specific payment link and you have its paymentLinkId, use our [Retrieve Payment Link](https://docs.payroc.com/api/schema/payment-links/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for only active links or multi-use links.
    ///
    /// Our gateway returns the following information about each payment link in the list:
    /// - **type** - Indicates whether the link can be used only once or if it can be used multiple times.
    /// - **authType** - Indicates whether the transaction is a sale or a pre-authorization.
    /// - **paymentMethods** - Indicates the payment method that the merchant accepts.
    /// - **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
    /// - **status** - Indicates if the payment link is active.
    ///
    /// For each payment link, we also return a paymentLinkId, which you can use for follow-on actions.
    /// </summary>
    Task<PayrocPager<PaymentLinkPaginatedListDataItem>> ListAsync(
        ListPaymentLinksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create a payment link that a customer can use to make a payment for goods or services.
    ///
    /// The request includes the following settings:
    /// - **type** - Indicates whether the link can be used only once or if it can be used multiple times.
    /// - **authType** - Indicates whether the transaction is a sale or a pre-authorization.
    /// - **paymentMethod** - Indicates the payment methods that the merchant accepts.
    /// - **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
    ///
    /// If your request is successful, our gateway returns a paymentLinkId, which you can use to perform follow-on actions.
    ///
    /// **Note:** To share the payment link with a customer, use our [Share Payment Link](https://docs.payroc.com/api/schema/payment-links/sharing-events/share) method.
    /// </summary>
    WithRawResponseTask<CreatePaymentLinksResponse> CreateAsync(
        CreatePaymentLinksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a payment link.
    ///
    /// To retrieve a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payment-links/list) method to search for the payment link.
    ///
    /// Our gateway returns the following information about the payment link:
    /// - **type** - Indicates whether the link can be used only once or if it can be used multiple times.
    /// - **authType** - Indicates whether the transaction is a sale or a pre-authorization.
    /// - **paymentMethods** - Indicates the payment method that the merchant accepts.
    /// - **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
    /// - **status** - Indicates if the payment link is active.
    /// </summary>
    WithRawResponseTask<RetrievePaymentLinksResponse> RetrieveAsync(
        RetrievePaymentLinksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to partially update a payment link. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a payment link, you need its paymentLinkId, which we sent you in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payment-links/list) method to search for the payment link.
    ///
    /// You can update the following properties of a multi-use link:
    /// - **expiresOn parameter** - Expiration date of the link.
    /// - **customLabels object** - Label for the payment button.
    /// - **credentialOnFile object** - Settings for saving the customer's payment details.
    ///
    /// You can update the following properties of a single-use link:
    /// - **expiresOn parameter** - Expiration date of the link.
    /// - **authType parameter** - Transaction type of the payment link.
    /// - **amount parameter** - Total amount of the transaction.
    /// - **currency parameter** - Currency of the transaction.
    /// - **description parameter** - Brief description of the transaction.
    /// - **customLabels object** - Label for the payment button.
    /// - **credentialOnFile object** - Settings for saving the customer's payment details.
    ///
    /// **Note:** When a merchant updates a single-use link, we update the payment URL and HTML code in the assets object. The customer can't use the original link to make a payment.
    /// </summary>
    WithRawResponseTask<PartiallyUpdatePaymentLinksResponse> PartiallyUpdateAsync(
        PartiallyUpdatePaymentLinksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to deactivate a payment link.
    ///
    /// To deactivate a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payment-links/list) method to search for the payment link.
    ///
    /// If your request is successful, our gateway deactivates the payment link. The customer can't use the link to make a payment, and you can't reactivate the payment link.
    /// </summary>
    WithRawResponseTask<DeactivatePaymentLinksResponse> DeactivateAsync(
        DeactivatePaymentLinksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
