using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

public partial interface IFundingRecipientsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding recipients linked to your account.
    ///
    /// Note: If you want to view the details of a specific funding recipient and you have its recipientId, use our [Retrieve Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/retrieve) method.
    ///
    /// Our gateway returns the following information about each funding recipient in the list:
    /// - Tax ID and Doing Business As (DBA) name.
    /// - Address and contact details.
    /// - Funding accounts linked to the funding recipient.
    ///
    /// For each funding recipient, we also return the recipientId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<FundingRecipient>> ListAsync(
        ListFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create a funding recipient.
    ///
    /// A funding recipient is a business or organization that can receive funds but can't run transactions, for example, a charity.
    ///
    /// In the request, include the following information:
    /// -	Legal information, including its tax ID, Doing Business As (DBA) name, and address.
    /// -	Contact information, including the email address.
    /// -	Owners' details, including their contact details.
    /// -	Funding account details.
    ///
    /// Our gateway returns the recipientId of the funding recipient, which you can use to run follow-on actions.
    /// </summary>
    WithRawResponseTask<FundingRecipient> CreateAsync(
        CreateFundingRecipient request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a funding recipient.
    ///
    /// To retrieve a funding recipient, you need its recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.
    ///
    /// **Note:** If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    ///
    /// Our gateway returns the following information about the funding recipient:
    ///
    /// - Tax ID and Doing Business As (DBA) name.
    /// - Address and contact details.
    /// - Funding accounts linked to the funding recipient.
    /// </summary>
    WithRawResponseTask<FundingRecipient> RetrieveAsync(
        RetrieveFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to update the details of a funding recipient. If a request contains significant changes, we might need to re-approve the funding recipient.
    ///
    /// To update a funding recipient, you need it's recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.
    ///
    /// **Note**: If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    ///
    /// You can update the following details of a funding recipient:
    /// - Doing Business As (DBA) name
    /// - Tax ID and charity ID
    /// - Address and contact methods
    /// </summary>
    Task UpdateAsync(
        UpdateFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to delete a funding recipient, including its funding accounts and owners.
    ///
    /// To delete a funding recipient, you need its recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.
    ///
    /// **Note**: If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    /// </summary>
    Task DeleteAsync(
        DeleteFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use  this method to return a list of funding accounts associated with a funding recipient.
    ///
    /// **Note:** If you want to view the details of a specific funding account and you have its fundingAccountId, use our [Retrieve Funding Account](https://docs.payroc.com/api/schema/funding/funding-accounts/retrieve) method.
    ///
    /// To retrieve the funding accounts associated with a funding recipient, you need the recipientId. If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    ///
    /// Our gateway returns the following information about each funding account:
    /// -	Name of the account holder.
    /// -	ACH details for the account.
    /// -	Status of the account.
    ///
    /// Our gateway also returns the fundingAccountId, which you can use to run follow-on actions.
    /// </summary>
    WithRawResponseTask<IEnumerable<FundingAccount>> ListAccountsAsync(
        ListFundingRecipientFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to create a funding account and add it to a funding recipient.
    ///
    /// To add a funding account to a funding recipient, you need the recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.
    ///
    /// **Note:** If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    ///
    /// In the request, include the following information:
    /// -	Account type, for example, if the account is a checking or savings account.
    /// -	Account holder's name.
    /// -	ACH information, including the routing number and account number of the account.
    ///
    /// Our gateway returns the fundingAccountId, which you can use to run follow-on actions.
    /// </summary>
    WithRawResponseTask<FundingAccount> CreateAccountAsync(
        CreateAccountFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a list of owners of a funding recipient.
    ///
    /// **Note:** If you want to view the details of a specific owner and you have their ownerId, use our [Retrieve Owner](https://docs.payroc.com/api/schema/boarding/owners/retrieve) method.
    ///
    /// To list the owners of a funding recipient, you need its recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method. If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    ///
    /// Our gateway returns the following information about each owner in the list:
    /// -	Name, date of birth, and address.
    /// -	Contact details, including their email address.
    /// -	Relationship to the funding recipient.
    ///
    /// Our gateway also returns the ownerId, which you can use to perform follow-on actions.
    /// </summary>
    WithRawResponseTask<IEnumerable<Owner>> ListOwnersAsync(
        ListFundingRecipientOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to add an additional owner to a funding recipient.
    ///
    /// To add an owner to a funding recipient, you need the recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.
    ///
    /// **Note:** If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    ///
    /// In the request, include the following information about the owner:
    ///
    /// - Name, date of birth, and address.
    /// - Contact details, including their email address.
    /// - Relationship to the funding recipient, including whether they are a control prong.
    ///
    /// In the response, our gateway returns the ownerId, which you can use to run follow-on actions.
    /// </summary>
    WithRawResponseTask<Owner> CreateOwnerAsync(
        CreateOwnerFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
