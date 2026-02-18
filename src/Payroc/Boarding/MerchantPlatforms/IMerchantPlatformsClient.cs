using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.MerchantPlatforms;

public partial interface IMerchantPlatformsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of merchant platforms that are linked to your ISV account.
    ///
    /// **Note**: If you want to view the details of a specific merchant platform and you have its merchantPlatformId, use our [Retrieve Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/retrieve) method.
    ///
    /// Our gateway returns the following information about each merchant platform in the list:
    /// - Legal information, including its legal name and address.
    /// - Contact information, including the email address for the business.
    /// - Processing  account information, including the processingAccountId and status of each processing account that's linked to the merchant platform.
    ///
    /// For each merchant platform, we also return its merchantPlatformId and its linked processingAccountIds, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<MerchantPlatform>> ListAsync(
        ListMerchantPlatformsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to board a merchant with Payroc.
    ///
    /// **Note**: This method is part of our Boarding solution. To help you understand how this method works with other Boarding methods, go to [Board a Merchant](https://docs.payroc.com/guides/board-merchants/boarding).
    ///
    /// In the request, include the following information:
    /// - Legal information, including its legal name and address.
    /// - Contact information, including the email address for the business.
    /// - Processing account information, including the pricing model, owners, and contacts for the processing account.
    ///
    /// When you send a successful request, we review the merchant's information. After we complete our review and approve the merchant, we assign:
    /// - **merchantPlatformId** - Unique identifier for the merchant platform.
    /// - **processingAccountId** - Unique identifier for each processing account linked to the merchant platform.
    ///
    /// You need to keep these to perform follow-on actions, for example, you need the processingAccountId to [order terminals](https://docs.payroc.com/api/schema/boarding/processing-accounts/create-terminal-order) for the processing account.
    /// </summary>
    WithRawResponseTask<MerchantPlatform> CreateAsync(
        CreateMerchantAccount request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a merchant platform.
    ///
    /// To retrieve a merchant platform, you need its merchantPlatformId. Our gateway returned the merchantPlatformId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method.
    ///
    /// **Note:** If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.
    ///
    /// Our gateway returns the following information about the merchant platform:
    /// -	Legal information, including its legal name and address.
    /// -	Contact information, including the email address for the business.
    /// -	Processing account information, including the processingAccountId and status of each processing account that's linked to the merchant platform.
    /// </summary>
    WithRawResponseTask<MerchantPlatform> RetrieveAsync(
        RetrieveMerchantPlatformsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of processing accounts linked to a merchant platform.
    ///
    /// **Note**: If you want to view the details of a specific processing account and you have its processingAccountId, use our [Retrieve Processing Account](https://docs.payroc.com/api/schema/boarding/processing-accounts/retrieve) method.
    ///
    /// Use the query parameters to filter the list of results that we return, for example, to search for only closed processing accounts.
    ///
    /// To list the processing accounts for a merchant platform, you need its merchantPlatformId. If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.
    ///
    /// Our gateway returns the following information about eahc processing account in the list:
    /// - Business details, including its status, time zone, and address.
    /// - Owners' details, including their contact details.
    /// - Funding, pricing, and processing information, including its pricing model and funding accounts.
    ///
    /// For each processing account, we also return its processingAccountId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<ProcessingAccount>> ListProcessingAccountsAsync(
        ListBoardingMerchantPlatformProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to add an additional processing account to a merchant platform.
    ///
    /// To add a processing account to a merchant platform, you need the merchantPlatformId. Our gateway returned the merchantPlatformId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method.
    ///
    /// **Note**: If you don't have the merchantPlatformId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for the merchant platform.
    ///
    /// In the request, include the following information:
    /// - Business details, including its business type, time zone, and address.
    /// - Owners' details, including their contact details.
    /// - Funding, pricing, and processing information, including its pricing model and funding accounts.
    ///
    /// When you send a successful request, we review the information about the processing account. After we complete our review and approve the processing account, we assign a processingAccountId, which you need to perform follow-on actions.
    ///
    /// **Note**: You can subscribe to our processingAccount.status.changed event to get notifications when we update the status of a processing account. For more information about how to subscribe to events, go to [Events List](https://docs.payroc.com/knowledge/events/events-list).
    /// </summary>
    WithRawResponseTask<ProcessingAccount> CreateProcessingAccountAsync(
        CreateProcessingAccountMerchantPlatformsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
