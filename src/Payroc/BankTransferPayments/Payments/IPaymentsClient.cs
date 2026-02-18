using Payroc;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Payments;

public partial interface IPaymentsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments.
    ///
    /// **Note:** If you want to view the details of a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for payments for a customer, a date range, or a settlement state.
    ///
    /// Our gateway returns the following information about each payment in the list:
    ///
    /// - Order details, including the transaction amount and when it was processed.
    /// - Bank account details, including the customer’s name and account number.
    /// - Customer's details, including the customer’s phone number.
    /// - Transaction details, including any refunds or re-presentments.
    ///
    /// For each transaction, we also return the paymentId and an optional secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<BankTransferPayment>> ListAsync(
        ListPaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to run a sale with a customer's bank account details.
    ///
    /// In the response, our gateway returns information about the bank transfer payment and a paymentId, which you need for the following methods:
    /// -	[Retrieve payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) - View the details of the bank transfer payment.
    /// -	[Reverse payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/reverse-payment) - Cancel the bank transfer payment if it's an open batch.
    /// -	[Refund payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/refund) - Run a referenced refund to return funds to the customer's bank account.
    ///
    /// **Payment methods**
    ///
    /// Our gateway accepts the following payment methods:
    /// -	Automated clearing house (ACH) details
    /// -	Pre-authorized debit (PAD) details
    ///
    /// You can also use [secure tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/overview) and [single-use tokens](https://docs.payroc.com/api/schema/tokenization/single-use-tokens/create) that you created from ACH details or PAD details.
    /// </summary>
    WithRawResponseTask<BankTransferPayment> CreateAsync(
        BankTransferPaymentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a bank transfer payment.
    ///
    /// To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/create) method.
    ///
    /// Note: If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/list) method to search for the payment.
    ///
    /// Our gateway returns the following information about the payment:
    ///
    /// -	Order details, including the transaction amount and when it was processed.
    /// -	Bank account details, including the customer’s name and account number.
    /// -	Customer’s details, including the customer’s phone number.
    /// -	Transaction details, including any refunds or re-presentments.
    ///
    /// If the merchant saved the customer’s bank account details, our gateway returns a secureTokenID, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<BankTransferPayment> RetrieveAsync(
        RetrievePaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to re-present an ACH payment.
    ///
    /// To re-present a payment, you need the paymentId of the return. To get the paymentId of the return, complete the following steps:
    ///
    /// 1.	Use our [Retrieve Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) method  to view the details of the original payment.
    /// 2.	From the [returns object](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve#response.body.returns) in the response, get the paymentId of the return.
    ///
    /// Our gateway uses the bank account details from the original payment. If you want to update the customer's bank account details, send the new bank account details in the request.
    ///
    /// If your request is successful, our gateway re-presents the payment.
    /// </summary>
    WithRawResponseTask<BankTransferPayment> RepresentAsync(
        Representment request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
