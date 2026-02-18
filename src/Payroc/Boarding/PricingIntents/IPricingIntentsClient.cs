using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.PricingIntents;

public partial interface IPricingIntentsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of pricing intents associated with the ISV.
    ///
    /// **Note:** If you want to view the details of a specific pricing intent and you have its pricingIntentId, use our [Retrieve Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/retrieve) method.
    ///
    /// Our gateway returns the following information about each pricing intent in the list:
    ///
    /// - Information about the fees, including the base fees, gateway fees, and processor fees.
    /// - Status of the pricing intent, including whether we approved the pricing intent.
    ///
    /// For each pricing intent, we also return its pricingIntentId which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<PricingIntent50>> ListAsync(
        ListPricingIntentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create a pricing intent that you can assign to a processing account.
    ///
    /// In the request, you must provide the following:
    /// -	Processing fees, including the pricing program and the fee to process each transaction.
    /// -	Gateway fees, including the fee for each transaction handled by our gateway.
    /// -	Base fees, including maintenance and PCI fees.
    ///
    /// In the response, our gateway returns information about the pricing intent and the pricingIntentId, which you need for the following methods:
    /// -	[Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) - Assign the pricing intent to a processing account, when you create the merchant platform and its processing accounts.
    /// -	[Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) - Assign the pricing intent to a processing account.
    /// -	[Retrieve Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/retrieve) - Retrieve information about a pricing intent.
    /// -	[Update Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/update) - Update the details of a pricing intent.
    /// -	[Delete Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/delete) - Delete a pricing intent.
    /// -	[Partially Update Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/partially-update) - Partially update the details of a pricing intent.
    /// </summary>
    WithRawResponseTask<PricingIntent50> CreateAsync(
        CreatePricingIntentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a pricing intent.
    ///
    /// To retrieve a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.
    ///
    /// **Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.
    ///
    /// Our gateway returns the following information about the pricing intent:
    ///
    /// - Information about the fees, including the base fees, gateway fees, and processor fees.
    /// - Status of the pricing intent, including whether we approved the pricing intent.
    /// </summary>
    WithRawResponseTask<PricingIntent50> RetrieveAsync(
        RetrievePricingIntentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to update the details of a pricing intent. If you update a pricing intent, it won't affect merchant that you've previously onboarded.
    ///
    /// To update a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.
    ///
    /// **Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.
    ///
    /// You can update the following details about a pricing intent:
    ///
    /// - Fees, including the base fees, processor fees, and gateway fees.
    /// - Custom name for the pricing intent.
    /// - Additional services that merchants can sign up for.
    /// </summary>
    Task UpdateAsync(
        UpdatePricingIntentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to delete a pricing intent.
    ///
    /// &gt; **Important:** When you delete a pricing intent, you can't recover it. You also won't be able to assign the pricing intent to a merchant's boarding application.
    ///
    /// To delete a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.
    ///
    /// **Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.
    /// </summary>
    Task DeleteAsync(
        DeletePricingIntentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to partially update a pricing intent. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// If you update a pricing intent, it won't affect merchants you've previously onboarded.
    ///
    /// To update a pricing intent, you need its pricingIntentId. Our gateway returned the pricingIntentId in the response of the [Create Pricing Intent](https://docs.payroc.com/api/schema/boarding/pricing-intents/create) method.
    ///
    /// **Note:** If you don't have the pricingIntentId, use our [List Pricing Intents](https://docs.payroc.com/api/schema/boarding/pricing-intents/list) method to search for the pricing intent.
    ///
    /// You can update the following details about a pricing intent:
    ///
    /// - Fees, including the base fees, processor fees, and gateway fees.
    /// - Custom name for the pricing intent.
    /// - Additional services that merchants can sign up for.
    /// </summary>
    WithRawResponseTask<PricingIntent50> PartiallyUpdateAsync(
        PartiallyUpdatePricingIntentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
