using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingActivity;

public partial interface IFundingActivityClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding balances available for each merchant linked to your account.
    ///
    /// Use query parameters to filter the list of results we return, for example, to search for the funding balance for a specific merchant.
    ///
    /// Our gateway returns the following information about each merchant in the list:
    /// - Total funds for the merchant.
    /// - Available funds that you can use for funding instructions.
    /// - Pending funds that we have not yet sent to funding accounts.
    /// </summary>
    WithRawResponseTask<RetrieveBalanceFundingActivityResponse> RetrieveBalanceAsync(
        RetrieveBalanceFundingActivityRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of activity associated with your merchants' funding balances within a specific date range.
    ///
    /// Use query parameters to filter the list of results we return, for example, to view the activity for a specific merchant's funding balance.
    ///
    /// Our gateway returns the following information about each activity in the list:
    /// - Name of the merchant who owns the funding balance.
    /// -	Amount of funds added or removed from the funding balance.
    /// -	Funding account that received funds from the funding balance.
    /// </summary>
    Task<PayrocPager<ActivityRecord>> ListAsync(
        ListFundingActivityRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
