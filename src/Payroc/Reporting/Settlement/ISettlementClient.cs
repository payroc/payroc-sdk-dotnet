using Payroc;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

public partial interface ISettlementClient
{
    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of batches that your merchants submitted to the processor on a specific date.
    ///
    /// **Note:** If you want to view the details of a specific batch and you have its batchId, use our [Retrieve Batch](https://docs.payroc.com/api/schema/reporting/settlement/retrieve-batch) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for batches that were submitted by a specific merchant.
    ///
    /// &gt; **Important:** You must provide a value for the date query parameter.
    ///
    /// Our gateway returns the following information about each batch in the list:
    /// -	Transaction information, including the number of transactions and total value of sales.
    /// -	Merchant information, including the merchant ID (MID) and the processing account that the batch is associated with.
    /// </summary>
    Task<PayrocPager<Batch>> ListBatchesAsync(
        ListReportingSettlementBatchesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a batch.
    ///
    /// **Note:** To retrieve a batch, you need its batchId. If you don't have the batchId, use our [List Batches](https://docs.payroc.com/api/schema/reporting/settlement/list-batches) method to search for the batch.
    ///
    /// Our gateway returns the following information about the batch:
    ///
    /// -	Transaction information, including the number of transactions and total value of sales.
    /// -	Merchant information, including the merchant ID (MID) and the processing account that the batch is associated with.
    /// </summary>
    WithRawResponseTask<Batch> RetrieveBatchAsync(
        RetrieveBatchSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a paginated list of your merchants’ transactions.
    ///
    /// **Note:** If you want to view the details of a specific transaction and you have its transactionId, use our [Retrieve Transaction](https://docs.payroc.com/api/schema/reporting/settlement/retrieve-transaction) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for transactions for a specific merchant.
    ///
    /// &gt; **Important:** You must provide a value for either the date query parameter or the batchId query parameter.
    ///
    /// Our gateway returns the following information about each transaction in the list:
    ///
    /// -	Merchant and processing account that ran the transaction.
    /// -	Transaction type, date, amount, and the payment method that the customer used.
    /// -	Batch that contains the transaction, and authorization details for the transaction.
    /// -	Processor that settled the transaction and the ACH deposit containing the transaction.
    /// </summary>
    Task<PayrocPager<Transaction>> ListTransactionsAsync(
        ListReportingSettlementTransactionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about a transaction.
    ///
    /// **Note:** To retrieve a transaction, you need its transactionId. If you don't have the transactionId, use our [List Transactions](https://docs.payroc.com/api/schema/reporting/settlement/list-transactions) method to search for the transaction.
    ///
    /// Our gateway returns the following information about the transaction:
    ///
    /// -	Merchant and processing account that ran the transaction.
    /// -	Transaction type, date, amount, and the payment method that the customer used.
    /// -	Batch that contains the transaction, and authorization details for the transaction.
    /// -	Processor that settled the transaction and the ACH deposit containing the transaction.
    /// </summary>
    WithRawResponseTask<Transaction> RetrieveTransactionAsync(
        RetrieveTransactionSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of authorizations.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for authorizations linked to a specific merchant.
    ///
    /// &gt; **Important:** You must provide a value for either the date query parameter or the batchId query parameter.
    ///
    /// Our gateway returns the following information about each authorization in the list:
    /// - Authorization response from the issuing bank.
    /// - Amount that the issuing bank authorized.
    /// - Merchant that ran the authorization.
    /// - Details about the customer's card, the transaction, and the batch.
    /// </summary>
    Task<PayrocPager<Authorization>> ListAuthorizationsAsync(
        ListReportingSettlementAuthorizationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about an authorization.
    ///
    /// **Note:** To retrieve an authorization, you need its authorizationId. If you don't have the authorizationId, use our [List Authorizations](https://docs.payroc.com/api/schema/reporting/settlement/list-authorizations) method to search for the authorization.
    ///
    /// Our gateway returns the following information about the authorization:
    /// - Authorization response from the issuing bank.
    /// - Amount that the issuing bank authorized.
    /// - Merchant that ran the authorization.
    /// - Details about the customer's card, the transaction, and the batch.
    /// </summary>
    WithRawResponseTask<Authorization> RetrieveAuthorizationAsync(
        RetrieveAuthorizationSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of disputes.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for disputes linked to a specific merchant.
    ///
    /// &gt; **Important:** You must provide a value for the date query parameter.
    ///
    /// Our gateway returns the following information about each dispute in the list:
    /// - Its status, type, and description.
    /// - Transaction that the dispute is linked to, including the transaction date, merchant who ran the transaction, and the payment method that the cardholder used.
    /// </summary>
    Task<PayrocPager<Dispute>> ListDisputesAsync(
        ListReportingSettlementDisputesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return the status history of a dispute.
    ///
    /// To view the status history of a dispute, you need its disputeId. If you don't have the disputeId, use our [List Disputes](https://docs.payroc.com/api/schema/reporting/settlement/list-disputes) method to search for the dispute.
    ///
    /// Our gateway returns a list that contains each status change, the date it was changed, and its updated status.
    /// </summary>
    WithRawResponseTask<IEnumerable<DisputeStatus>> ListDisputesStatusesAsync(
        ListDisputesStatusesSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of ACH deposits that we paid to your merchants.
    ///
    /// **Note:** If you want to view the details of a specific ACH deposit and you have its achDepositId, use our [Retrieve ACH Deposit](https://docs.payroc.com/api/schema/reporting/settlement/retrieve-ach-deposit) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for ACH deposits that we paid to a specific merchant.
    ///
    /// &gt; **Important:** You must provide a value for the date query parameter.
    ///
    /// Our gateway returns the following information about each ACH deposit in the list:
    /// - Merchant that we sent the ACH deposit to.
    /// - Total amount that we paid the merchant.
    /// - Breakdown of sales, returns, and fees.
    /// </summary>
    Task<PayrocPager<AchDeposit>> ListAchDepositsAsync(
        ListReportingSettlementAchDepositsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Use this method to retrieve information about an ACH deposit that we paid to a merchant.
    ///
    /// **Note:** To retrieve an ACH deposit, you need its achDepositId. If you don't have the achDepositId, use our [List ACH Deposits](https://docs.payroc.com/api/schema/reporting/settlement/list-ach-deposits) method to search for the ACH deposit.
    ///
    /// Our gateway returns the following information about the ACH deposit:
    ///
    /// - Merchant that we sent the ACH deposit to.
    /// - Total amount that we paid the merchant.
    /// - Breakdown of sales, returns, and fees.
    /// </summary>
    WithRawResponseTask<AchDeposit> RetrieveAchDepositAsync(
        RetrieveAchDepositSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Retrieve a list of ACH deposit fees.
    ///
    /// &gt; **Important:** You must provide a value for either the 'date' query parameter or the 'achDepositId' query parameter.
    /// </summary>
    Task<PayrocPager<AchDepositFee>> ListAchDepositFeesAsync(
        ListReportingSettlementAchDepositFeesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
