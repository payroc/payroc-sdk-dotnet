using Payroc;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Refunds;

public partial interface IRefundsClient
{
    /// <summary>
    /// Use this method to cancel a bank transfer payment in an open batch. This is also known as voiding a payment.
    ///
    /// To cancel a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/list) method to search for the bank transfer payment.
    ///
    /// If your request is successful, our gateway removes the bank transfer payment from the merchant’s open batch and no funds are taken from the customer's bank account.
    /// </summary>
    WithRawResponseTask<BankTransferPayment> ReversePaymentAsync(
        ReversePaymentRefundsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to refund a bank transfer payment that is in a closed batch.
    ///
    /// To refund a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/create) method.
    ///
    /// **Note:** If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/list) method to search for the bank transfer payment.
    ///
    /// If your refund is successful, our gateway returns the payment amount to the customer's account.
    ///
    /// **Things to consider**
    /// - If the merchant refunds a bank transfer payment that is in an open batch, our gateway reverses the bank transfer payment.
    /// - Some merchants can run unreferenced refunds, which means that they don’t need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create).
    /// </summary>
    WithRawResponseTask<BankTransferPayment> RefundAsync(
        BankTransferReferencedRefund request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of bank transfer refunds.
    ///
    /// **Note:** If you want to view the details of a specific refund and you have its refundId, use our [Retrieve Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for refunds for a customer, an orderId, or a date range.
    ///
    /// Our gateway returns the following information about each refund in the list:
    ///
    /// -	Order details, including the refund amount and when it was processed.
    /// -	Bank account details, including the customer’s name and account number.
    ///
    /// For referenced refunds, our gateway also returns details about the payment that the refund is linked to.
    /// </summary>
    Task<PayrocPager<BankTransferRefund>> ListAsync(
        ListRefundsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create an unreferenced refund. An unreferenced refund is a refund that isn’t linked to a bank transfer payment.
    ///
    /// **Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer’s original payment method and links the refund to the payment.
    ///
    /// In the request, you must provide the customer’s payment method and information about the order including the refund amount.
    ///
    /// In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:
    ///
    /// -	[Retrieve refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/retrieve) – View the details of the refund.
    /// -	[Reverse refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/reverse-refund) – Cancel the refund if it’s in an open batch.
    /// </summary>
    WithRawResponseTask<BankTransferRefund> CreateAsync(
        BankTransferUnreferencedRefund request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a refund.
    ///
    /// To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/refund) method or the [Create Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create) method.
    ///
    /// **Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/list) method to search for the refund.
    ///
    /// Our gateway returns the following information about the refund:
    ///
    /// - Order details, including the refund amount and when it was processed.
    /// - Bank account details, including the customer’s name and account number.
    ///
    /// If the refund is a referenced refund, our gateway also returns details about the payment that the refund is linked to.
    /// </summary>
    WithRawResponseTask<BankTransferRefund> RetrieveAsync(
        RetrieveRefundsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to cancel a bank transfer refund in an open batch.
    ///
    /// To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/refund) or [Create Refund](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/create) method.
    ///
    /// **Note:** If you don’t have the refundId, use our [List Refunds](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/list) method to search for the refund.
    ///
    /// If your request is successful, the gateway removes the refund from the merchant’s open batch, and no funds are returned to the cardholder’s account.
    /// </summary>
    WithRawResponseTask<BankTransferRefund> ReverseRefundAsync(
        ReverseRefundRefundsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
