using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingAccounts;

public partial interface IFundingAccountsClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding accounts associated with your account.
    ///
    /// **Note:** If you want to view the details of a specific funding account and you have its fundingAccountId, use our [Retrieve Funding Account](https://docs.payroc.com/api/schema/funding/funding-accounts/retrieve) method.
    ///
    /// Our gateway returns the following information about each funding account in the list:
    /// - Name of the account holder and ACH details for the account.
    /// - Status of the account.
    /// - Whether we send funds to the account, withdraw funds from the account, or both.
    ///
    /// For each funding account, we also return the fundingAccountId, which you can use to perform follow-on actions.
    /// </summary>
    Task<PayrocPager<FundingAccount>> ListAsync(
        ListFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a funding account.
    ///
    /// To retrieve a funding account, you need its fundingAccountId. Our gateway returned the fundingAccountId when you created the funding account.
    ///
    /// **Note:** If you don't have the fundingAccountId, use our [List Funding Accounts](https://docs.payroc.com/api/schema/funding/funding-accounts/list) method to search for the account.
    ///
    /// Our gateway returns the following information about the funding account:
    /// - Name of the account holder and ACH details for the account.
    /// - Status of the account.
    /// - Whether we send funds to the account, withdraw funds from the account, or both.
    /// </summary>
    WithRawResponseTask<FundingAccount> RetrieveAsync(
        RetrieveFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// &gt; **Important:** You can't update the details of a funding account that is associated with a processing account.
    ///
    /// Use this method to update the details of a funding account that is associated with a funding recipient.
    ///
    /// To update a funding account, you need its fundingAccountId. Our gateway returned the fundingAccountId when you created the funding account.
    ///
    /// **Note:** If you don’t have the fundingAccountId, use our [List Funding Accounts](https://docs.payroc.com/api/schema/funding/funding-accounts/list) method to search for the funding account.
    ///
    /// You can update the following details about the funding account:
    /// -	Account type.
    /// -	Account holder's name.
    /// -	ACH information for the account.
    /// </summary>
    Task UpdateAsync(
        UpdateFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// &gt; **Important:** You can't delete a funding account that is associated with a processing account.
    ///
    /// Use this method to delete a funding account that is associated with a funding recipient.
    ///
    /// To delete a funding account, you need its fundingAccountId. Our gateway returned the fundingAccountId when you created the funding account.
    ///
    /// **Note:** If you don't have the fundingAccountId, use our [List Funding Accounts](https://docs.payroc.com/api/schema/funding/funding-accounts/list) method to search for the funding account.
    /// </summary>
    Task DeleteAsync(
        DeleteFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
