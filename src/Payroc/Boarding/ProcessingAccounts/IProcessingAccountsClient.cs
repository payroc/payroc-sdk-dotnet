using OneOf;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

public partial interface IProcessingAccountsClient
{
    /// <summary>
    /// Use this method to retrieve information about a specific processing account.
    ///
    /// To retrieve a processing account, you need its processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// **Note:** If you don't have the processingAccountId, use our [List Merchant Platform's Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.
    ///
    /// Our gateway returns the following information about the processing account:
    ///
    /// -	Business information, including the Merchant Category Code (MCC), status of the processing account, and address of the business.
    /// -	Processing information, including the merchant’s refund policies and card types that the merchant accepts.
    /// -	Funding information, including funding schedules, funding fees, and details for the merchant’s funding accounts.
    /// -	Pricing information, including [HATEOAS](https://docs.payroc.com/knowledge/basic-concepts/hypermedia-as-the-engine-of-application-state-hateoas) links to retrieve the pricing program for the processing account.
    /// </summary>
    WithRawResponseTask<ProcessingAccount> RetrieveAsync(
        RetrieveProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a list of funding accounts linked to a processing acccount.
    ///
    /// To retrieve a list of funding accounts for a processing account, you need the processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or the [Create Proccessing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// Our gateway returns information about the following for each funding account in the list:
    /// - Account information, including the name on the account and payment methods.
    /// - Status, including whether we have approved or rejected the account.
    ///
    /// For each funding account, we also return its fundingAccountId, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<IEnumerable<FundingAccount>> ListProcessingAccountFundingAccountsAsync(
        ListProcessingAccountFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a list of contacts for a processing account.
    ///
    /// **Note:** If you want to view the details of a specific contact and you have their contactId, use our [Retrieve Contact](https://docs.payroc.com/api/schema/boarding/contacts/retrieve) method.
    ///
    /// To list contacts for a processing account, you need the processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// Our gateway returns the following information about each contact:
    ///
    /// - Name and contact method, including their phone number or mobile number.
    /// - Role within the business, for example, if they are a manager.
    ///
    /// For each contact, we also return a contactId, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<PaginatedContacts> ListContactsAsync(
        ListContactsProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve the pricing agreement that we apply to a processing account.
    ///
    /// To retrieve the pricing agreement of a processing account, you need the processingAccountId. Our gateway returned the processingAccountId in the response to the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method and [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// **Note:** If you don't have the processingAccountId, use our [List Merchant Platform’s Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.
    ///
    /// Our gateway returns the following information about the pricing agreement that we apply to the processing account:
    ///
    /// - Base fees, including the annual fee and the fees for each chargeback and retrieval.
    /// - Processor fees, including the fees that we apply for processing card and ACH payments.
    /// - Gateway fees, including the setup fee and the fees for each transaction.
    /// - Service fees, including the fee that we apply if the merchant has signed up to a Hardware Advantage Plan.
    /// </summary>
    WithRawResponseTask<
        OneOf<PricingAgreementUs40, PricingAgreementUs50>
    > GetProcessingAccountPricingAgreementAsync(
        GetProcessingAccountPricingAgreementProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a list of owners of a processing account.
    ///
    /// **Note:** If you want to view the details of a specific owner and you have the ownerId, go to our [Retrieve Owner](https://docs.payroc.com/api/schema/boarding/owners/retrieve) method.
    ///
    /// To list the owners of a processing account, you need its processingAccountId. If you don't have the processingAccountId, use our [List Merchant Platform's Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.
    ///
    /// Our gateway returns the following information about each owner in the list:
    ///
    /// - Name, date of birth, and address.
    /// - Contact details, including their email address.
    /// - Relationship to the business, including whether they are a control prong or authorized signatory, and their equity stake in the business.
    /// </summary>
    Task<PayrocPager<Owner>> ListOwnersAsync(
        ListProcessingAccountOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to prompt a merchant to sign their pricing agreement.
    ///
    /// You can create a reminder only if you requested the merchant’s signature by email when you created the processing account for the merchant.
    ///
    /// To create a reminder, you need the processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
    ///
    /// **Note:** If you don’t know the processingAccountId, use our [List Merchant Platform’s Processing Accounts](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list-processing-accounts) method to search for the processing account.
    ///
    /// When you send a successful request, we send an email to the merchant that prompts them to sign their pricing agreement.
    /// </summary>
    WithRawResponseTask<CreateReminderProcessingAccountsResponse> CreateReminderAsync(
        CreateReminderProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of terminal orders associated with a processing account.
    ///
    /// **Note:** If you want to view the details of a specific terminal order and you have its terminalOrderId, use our [Retrieve Terminal Order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve) method.
    ///
    /// Use the query parameters to filter the list of results that we return, for example, to search for terminal orders by their status.
    ///
    /// To list the terminal orders for a processing account, you need its processingAccountId. If you don't have the processingAccountId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for a merchant platform and its processing accounts.
    ///
    /// Our gateway returns the following information for each terminal order in the list:
    ///
    /// - Status of the order
    /// - Items in the order
    /// - Training provider
    /// - Shipping information
    ///
    /// For each terminal order, we also return its terminalOrderId, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<IEnumerable<TerminalOrder>> ListTerminalOrdersAsync(
        ListTerminalOrdersProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to order and configure terminals for a processing account.
    ///
    /// **Note**: You need the ID of the processing account before you can create an order. If you don't know the processingAccountId, go to the [Retrieve a Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/retrieve) method.
    ///
    /// In the request, specify the gateway settings, device settings, and application settings for the terminal.
    ///
    /// In the response, our gateway returns information about the terminal order including its status and terminalOrderId that you can use to [retrieve the terminal order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve).
    ///
    /// **Note**: You can subscribe to the terminalOrder.status.changed event to get notifications when we update the status of a terminal order. For more information about how to subscribe to events, go to [Events Subscriptions](https://docs.payroc.com/guides/board-merchants/event-subscriptions).
    /// </summary>
    WithRawResponseTask<TerminalOrder> CreateTerminalOrderAsync(
        CreateTerminalOrder request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of processing terminals associated with a processing account.
    ///
    /// **Note:** If you want to view the details of a specific processing terminal and you have its processingTerminalId, use our [Retrieve Processing Terminal](https://docs.payroc.com/api/schema/boarding/processing-terminals/retrieve) method.
    ///
    /// To list the terminals for a processing account, you need its processingAccountId. If you don't have the processingAccountId, use our [List Merchant Platforms](https://docs.payroc.com/api/schema/boarding/merchant-platforms/list) method to search for a merchant platform and its processing accounts.
    ///
    /// Our gateway returns the following information for each processing terminal in the list:
    ///
    /// - Status indicating whether the terminal is active or inactive.
    /// - Configuration settings, including gateway settings and application settings.
    /// - Features, receipt settings, and security settings.
    /// - Devices that use the processing terminal's configuration.
    ///
    /// For each processing terminal, we also return its processingTerminalId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<ProcessingTerminal>> ListProcessingTerminalsAsync(
        ListProcessingTerminalsProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
