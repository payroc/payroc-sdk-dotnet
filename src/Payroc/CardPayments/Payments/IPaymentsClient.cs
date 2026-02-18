using Payroc;
using Payroc.Core;

namespace Payroc.CardPayments.Payments;

public partial interface IPaymentsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments.
    ///
    /// **Note:** If you want to view the details of a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/card-payments/payments/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for payments for a customer, a tip mode, or a date range.
    ///
    /// Our gateway returns the following information about each payment in the list:
    ///
    /// - Order details, including the transaction amount and when it was processed.
    /// - Payment card details, including the masked card number, expiry date, and payment method.
    /// - Cardholder details, including their contact information and shipping address.
    /// - Payment details, including the payment type, status, and response.
    ///
    /// For each transaction, we also return the paymentId and an optional secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<RetrievedPayment>> ListAsync(
        ListPaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to run a sale or a pre-authorization with a customer's payment card.
    ///
    /// In the response, our gateway returns information about the card payment and a paymentId, which you need for the following methods:
    ///
    /// -	[Retrieve payment](https://docs.payroc.com/api/schema/card-payments/payments/retrieve) - View the details of the card payment.
    /// -	[Adjust payment](https://docs.payroc.com/api/schema/card-payments/payments/adjust) - Update the details of the card payment.
    /// -	[Capture payment](https://docs.payroc.com/api/schema/card-payments/payments/capture)  - Capture the pre-authorization.
    /// -	[Reverse payment](https://docs.payroc.com/api/schema/card-payments/refunds/reverse)  - Cancel the card payment if it's in an open batch.
    /// -	[Refund payment](https://docs.payroc.com/api/schema/card-payments/refunds/create-referenced-refund)  - Run a referenced refund to return funds to the payment card.
    ///
    /// **Payment methods**
    ///
    /// - **Cards** - Credit, debit, and EBT
    /// - **Digital wallets** - [Apple Pay®](https://docs.payroc.com/guides/take-payments/apple-pay) and [Google Pay®](https://docs.payroc.com/guides/take-payments/google-pay)
    /// - **Tokens** - Secure tokens and single-use tokens
    ///
    /// **Features**
    ///
    /// Our Create Payment method also supports the following features:
    ///
    /// - [Repeat payments](https://docs.payroc.com/guides/take-payments/repeat-payments/use-your-own-software) - Run multiple payments as part of a payment schedule that you manage with your own software.
    /// - **Offline sales** - Run a sale or a pre-authorization if the terminal loses its connection to our gateway.
    /// - [Tokenization](https://docs.payroc.com/guides/take-payments/save-payment-details) - Save card details to use in future transactions.
    /// - [3-D Secure](https://docs.payroc.com/guides/take-payments/3-d-secure) - Verify the identity of the cardholder.
    /// - [Custom fields](https://docs.payroc.com/guides/take-payments/add-custom-fields) - Add your own data to a payment.
    /// - **Tips** - Add tips to the card payment.
    /// - **Taxes** - Add local taxes to the card payment.
    /// - **Surcharging** - Add a surcharge to the card payment.
    /// - **Dual pricing** - Offer different prices based on payment method, for example, if you use our RewardPay Choice pricing program.
    /// </summary>
    WithRawResponseTask<Payment> CreateAsync(
        PaymentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a card payment.
    ///
    /// To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.
    ///
    /// Our gateway returns the following information about the payment:
    ///
    /// - Order details, including the transaction amount and when it was processed.
    /// - Payment card details, including the masked card number, expiry date, and payment method.
    /// - Cardholder details, including their contact information and shipping address.
    /// - Payment details, including the payment type, status, and response.
    ///
    /// If the merchant saved the customer's card details, our gateway returns a secureTokenID, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<RetrievedPayment> RetrieveAsync(
        RetrievePaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to adjust a payment in an open batch.
    ///
    /// To adjust a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.
    ///
    /// You can adjust the following details of the payment:
    /// - Sale amount and tip amount
    /// - Payment status
    /// - Cardholder shipping address and contact information
    /// - Cardholder signature data
    ///
    /// Our gateway returns information about the adjusted payment, including information about the payment card and the cardholder.
    /// </summary>
    WithRawResponseTask<Payment> AdjustAsync(
        PaymentAdjustment request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to capture a pre-authorization.
    ///
    /// To capture a pre-authorization, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/card-payments/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/card-payments/payments/list) method to search for the payment.
    ///
    /// Depending on the amount you want to capture, complete the following:
    /// -	**Capture the full amount of the pre-authorization** - Don't send a value for the amount parameter in your request.
    /// -	**Capture less than the amount of the pre-authorization** - Send a value for the amount parameter in your request.
    /// -	**Capture more than the amount of the pre-authorization** - Adjust the pre-authorization before you capture it. For more information about adjusting a pre-authorization, go to [Adjust Payment](https://docs.payroc.com/api/schema/card-payments/payments/adjust).
    ///
    /// If your request is successful, our gateway takes the amount from the payment card.
    ///
    /// **Note:** For more information about pre-authorizations and captures, go to [Run a pre-authorization](https://docs.payroc.com/guides/integrate/run-a-pre-authorization).
    /// </summary>
    WithRawResponseTask<Payment> CaptureAsync(
        PaymentCapture request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
