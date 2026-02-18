using Payroc;
using Payroc.Core;

namespace Payroc.CardPayments.Refunds;

public partial interface IRefundsClient
{
    /// <summary>
    /// Use this method to cancel or to partially cancel a payment in an open batch. This is also known as voiding a payment.
    ///
    /// To cancel a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.
    ///
    /// If your request is successful, our gateway removes the payment from the merchant's open batch and no funds are taken from the cardholder's account.
    /// </summary>
    WithRawResponseTask<Payment> ReverseAsync(
        PaymentReversal request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to refund a payment that is in a closed batch.
    ///
    /// To refund a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.
    ///
    /// If your refund is successful, our gateway returns the payment amount to the cardholder's account.
    ///
    /// **Things to consider**
    ///
    /// - If the merchant refunds a payment that is in an open batch, our gateway reverses the payment.
    /// - Some merchants can run unreferenced refunds, which means that they don't need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund).
    /// </summary>
    WithRawResponseTask<Payment> CreateReferencedRefundAsync(
        ReferencedRefund request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of refunds.
    ///
    /// **Note:** If you want to view the details of a specific refund and you have its refundId, use our [Retrieve Refund](https://docs.payroc.com/api/schema/card-payments/refunds/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for refunds for a customer, a tender type, or a date range.
    /// Our gateway returns the following information about each refund in the list:
    /// - Order details, including the refund amount and when we processed the refund.
    /// - Payment card details, including the masked card number, expiry date, and payment method.
    /// - Cardholder details, including their contact information and shipping address.
    ///
    /// For referenced refunds, our gateway also returns details about the payment that the refund is linked to.
    /// </summary>
    Task<PayrocPager<RetrievedRefund>> ListAsync(
        ListRefundsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create an unreferenced refund. An unreferenced refund is a refund that isn't linked to a payment.
    ///
    /// **Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer's original payment method and links the refund to the payment.
    ///
    /// In the request, you must provide the customer's payment details and the refund amount.
    ///
    /// In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:
    ///
    /// - [Retrieve refund](https://docs.payroc.com/api/schema/card-payments/refunds/retrieve) - View the details of the refund.
    /// - [Adjust refund](https://docs.payroc.com/api/schema/card-payments/refunds/adjust) - Update the details of the refund.
    /// - [Reverse refund](https://docs.payroc.com/api/schema/card-payments/refunds/reverse-refund) - Cancel the refund if it's in an open batch.
    /// </summary>
    WithRawResponseTask<RetrievedRefund> CreateUnreferencedRefundAsync(
        UnreferencedRefund request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a refund.
    ///
    /// To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) method or the [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) method.
    ///
    /// **Note:** If you don't have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/card-payments/refunds/list) method to search for the refund.
    ///
    /// Our gateway returns the following information about the refund:
    /// - Order details, including the refund amount and when we processed the refund.
    /// - Payment card details, including the masked card number, expiry date, and payment method.
    /// - Cardholder details, including their contact information and shipping address.
    ///
    /// If the refund is a referenced refund, our gateway also returns details about the payment that the refund is linked to.
    /// </summary>
    WithRawResponseTask<RetrievedRefund> RetrieveAsync(
        RetrieveRefundsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to adjust a refund in an open batch.
    ///
    /// To adjust a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) method or the [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) method.
    ///
    /// **Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/card-payments/refunds/list) method to search for the refund.
    ///
    /// You can adjust the following details of the refund:
    /// - Customer details, including shipping address and contact information.
    /// - Status of the refund.
    ///
    /// Our gateway returns information about the adjusted refund, including:
    /// - Order details, including the refund amount and when we processed the refund.
    /// - Payment card details, including the masked card number, expiry date, and payment method.
    /// - Cardholder details, including their contact information and shipping address.
    ///
    /// If the refund is a referenced refund, our gateway also returns details about the payment that the refund is linked to.
    /// </summary>
    WithRawResponseTask<RetrievedRefund> AdjustAsync(
        RefundAdjustment request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to cancel a refund in an open batch.
    ///
    /// To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund) or [Create Refund](https://docs.payroc.com/api/schema/card-payments/refunds/create-unreferenced-refund) method.
    ///
    /// **Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/card-payments/refunds/list) method to search for the refund.
    ///
    /// If your request is successful, the gateway removes the refund from the merchant’s open batch and no funds are returned to the cardholder’s account.
    /// </summary>
    WithRawResponseTask<RetrievedRefund> ReverseRefundAsync(
        ReverseRefundRefundsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
