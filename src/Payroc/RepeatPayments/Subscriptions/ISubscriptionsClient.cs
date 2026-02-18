using Payroc;
using Payroc.Core;

namespace Payroc.RepeatPayments.Subscriptions;

public partial interface ISubscriptionsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of subscriptions.
    ///
    /// Note: If you want to view the details of a specific subscription and you have its subscriptionId, use our [Retrieve subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for subscriptions for a customer, a payment plan, or frequency.
    ///
    /// Our gateway returns information about the following for each subscription in the list:
    ///
    /// -	Payment plan the subscription is linked to.
    /// -	Secure token that represents cardholder’s payment details.
    /// -	Current state of the subscription, including its status, next due date, and invoices.
    /// -	Fees for setup and the cost of the recurring order.
    /// -	Subscription length, end date, and frequency.
    ///
    /// For each subscription, we also return the subscriptionId, the paymentPlanId, and the secureTokenId, which you can use to perform follow-actions.
    /// </summary>
    Task<PayrocPager<Subscription>> ListAsync(
        ListSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to assign a customer to a payment plan.
    ///
    /// **Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Payment plans endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/take-payments/repeat-payments).
    ///
    /// When you create a subscription you need to provide a unique subscriptionId that you use to run follow-on actions:
    ///
    /// - [Retrieve Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/retrieve) - View the details of the subscription.
    /// - [Update Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/partially-update) - Update the details of the subscription.
    /// - [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) - Stop taking payments for the subscription.
    /// - [Re-activate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/reactivate) - Start taking payments again for the subscription.
    /// - [Pay Manual Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/pay) - Manually collect a payment for the subscription.
    ///
    /// The request includes the following settings:
    /// - **paymentPlanId** - Unique identifier of the payment plan that the merchant wants to use. If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    /// - **paymentMethod** - Object that contains information about the secure token, which represents the customer's card details or bank account details.
    /// - **startDate** - Date that you want to start to take payments.
    ///
    /// You can also update the settings that the subscription inherited from the payment plan, for example, you can change the amount for each payment. If you change the settings for the subscription, it doesn't change the settings in the payment plan that it's linked to.
    /// </summary>
    WithRawResponseTask<Subscription> CreateAsync(
        SubscriptionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a subscription.
    ///
    /// To retrieve a subscription, you need its subscriptionId. You sent the subscriptionId in the request of the [Create subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// Our gateway returns information about the following for the subscription:
    ///
    /// -	Payment plan the subscription is linked to.
    /// -	Secure token that represents cardholder’s payment details.
    /// -	Current state of the subscription, including its status, next due date, and invoices.
    /// -	Fees for setup and the cost of the recurring order.
    /// -	Subscription length, end date, and frequency.
    ///
    /// We also return the paymentPlanId and the secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<Subscription> RetrieveAsync(
        RetrieveSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to partially update a subscription. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a subscription, you need its subscriptionId, which you sent in the request of the [Create subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the payment.
    ///
    /// You can update all of the properties of the subscription except for the following:
    ///
    /// **Can't delete**
    /// - recurringOrder
    /// - description
    /// - name
    ///
    /// **Can't perform any PATCH operation**
    /// - currentState
    /// - type
    /// - frequency
    /// - paymentPlan
    /// </summary>
    WithRawResponseTask<Subscription> PartiallyUpdateAsync(
        PartiallyUpdateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to deactivate a subscription.
    ///
    /// To deactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// If your request is successful, our gateway stops taking payments from the customer.
    ///
    /// To reactivate the subscription, use our [Reactivate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/reactivate) method.
    /// </summary>
    WithRawResponseTask<Subscription> DeactivateAsync(
        DeactivateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to reactivate a subscription.
    ///
    /// To reactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// If your request is successful, our gateway restarts taking payments from the customer.
    ///
    /// To deactivate the subscription, use our [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) method.
    /// </summary>
    WithRawResponseTask<Subscription> ReactivateAsync(
        ReactivateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to manually collect a payment linked to a subscription. You can manually collect a payment only if the merchant chose not to let our gateway automatically collect each payment.
    ///
    /// To manually collect a payment, you need the subscriptionId of the subscription that's linked to the payment. You sent the subscriptionId in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// The request includes an order object that contains information about the amount that you want to collect.
    ///
    /// In the response, our gateway returns information about the payment and a paymentId. You can use the paymentId in follow-on actions with the [Payments](https://docs.payroc.com/api/schema/card-payments/payments) endpoints or [Bank Transfer Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments) endpoints.
    /// </summary>
    WithRawResponseTask<SubscriptionPayment> PayAsync(
        SubscriptionPaymentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
