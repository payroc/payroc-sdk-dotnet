using Payroc;
using Payroc.Core;

namespace Payroc.RepeatPayments.PaymentPlans;

public partial interface IPaymentPlansClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payment plans for a processing terminal.
    ///
    /// **Note:** If you want to view the details of a specific payment plan and you have its paymentPlanId, use our [Retrieve Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/retrieve) method.
    ///
    /// Our gateway returns the following information about each payment plan in the list:
    ///
    ///   -	Name, length, and currency of the plan
    ///   -	How often our gateway collects each payment
    ///   -	How much our gateway collects for each payment
    ///   -	What happens if the merchant updates or deletes the plan
    ///
    /// For each payment plan, we return the paymentPlanId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<PaymentPlan>> ListAsync(
        ListPaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create a payment schedule that you can assign customers to.
    ///
    /// **Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Subscriptions endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/take-payments/repeat-payments).
    ///
    /// When you create a payment plan you need to provide a unique paymentPlanId that you use to run follow-on actions:
    ///
    /// -	[Retrieve Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/retrieve)  - View the details of the payment plan.
    /// -	[Update Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/partially-update)  - Update the details of the payment plan.
    /// -	[Delete Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/delete)  - Delete the payment plan.
    /// -	[Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create)  - Subscribe a customer to the payment plan.
    ///
    /// The request includes the following settings:
    ///
    /// -	**type** - Indicates if our gateway or the merchant collects payments. If the merchant manually collects payments, integrate with the [Pay Manual Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/pay) method.
    /// -	**recurringOrder** - Amount of each payment if the gateway automatically collect payments.
    /// -	**setupOrder** - Setup fee that our gateway immediately collects from the customer's payment method.
    /// -	**onUpdate and onDelete** - Indicates what happens to associated subscriptions if the merchant updates or deletes the payment plan.
    /// </summary>
    WithRawResponseTask<PaymentPlan> CreateAsync(
        CreatePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a payment plan.
    ///
    /// To retrieve a payment plan, you need its paymentPlanId. Our gateway returned the paymentPlanId in the response of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    ///
    /// Our gateway returns the following information about the payment plan:
    ///
    ///   -	Name, length, and currency of the plan
    ///   -	How often our gateway collects each payment
    ///   -	How much our gateway collects for each payment
    ///   -	What happens if the merchant updates or deletes the plan
    /// </summary>
    WithRawResponseTask<PaymentPlan> RetrieveAsync(
        RetrievePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to delete a payment plan.
    ///
    /// &gt; **Important:** When you delete a payment plan, you can’t recover it. You also won’t be able to add subscriptions to the payment plan.
    ///
    /// To delete a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    ///
    /// The value you sent for the onDelete parameter when you created the payment plan indicates what happens to associated subscriptions when you delete the plan:
    ///
    ///   -	`complete` - Our gateway stops taking payments for the subscriptions associated with the payment plan.
    ///   -	`continue` - Our gateway continues to take payments for the subscriptions associated with the payment plan. To stop a subscription for a cancelled payment plan, go to the [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) method.
    /// </summary>
    Task DeleteAsync(
        DeletePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to partially update a payment plan. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    ///
    /// You can update all of the properties of the payment plan except for the paymentPlanId.
    ///
    /// The value you sent for the onUpdate parameter when you created the payment plan indicates what happens to the associated subscriptions when you update the plan:
    /// - `update` - Our gateway updates the subscriptions associated with the payment plan.
    /// - `continue` - Our  gateway doesn't update the subscriptions associated with the payment plan.
    /// </summary>
    WithRawResponseTask<PaymentPlan> PartiallyUpdateAsync(
        PartiallyUpdatePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
