using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

public partial class SettlementClient
{
    private RawClient _client;

    internal SettlementClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of your merchants’ batches that were submitted to the processor on a specific date.
    ///
    /// **Note:** If you want to view a specific batch and you have its batchId, use our [Retrieve Batch](https://docs.payroc.com/api/schema/reporting/settlement/retrieve-batch) method.
    ///
    /// In your request, include the date that the batch was submitted. You can also use a query parameter to view the batches of a specific merchant.
    ///
    /// Our gateway returns the following information about each batch in the list:
    ///
    /// -	Transaction information, including the number of transactions and total value of sales.
    /// -	Merchant information, including the merchant ID (MID) and the processing account that the batch is associated with.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.ListBatchesAsync(
    ///     new ListReportingSettlementBatchesRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Date = new DateOnly(2027, 7, 2),
    ///         MerchantId = "4525644354",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<Batch>> ListBatchesAsync(
        ListReportingSettlementBatchesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["date"] = request.Date.ToString(Constants.DateFormat);
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                if (request.MerchantId != null)
                {
                    _query["merchantId"] = request.MerchantId;
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "batches",
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
                    }

                    {
                        var responseBody = await response.Raw.Content.ReadAsStringAsync();
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case 400:
                                    throw new BadRequestError(
                                        JsonUtils.Deserialize<FourHundred>(responseBody)
                                    );
                                case 401:
                                    throw new UnauthorizedError(
                                        JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                    );
                                case 403:
                                    throw new ForbiddenError(
                                        JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                    );
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                    );
                                case 500:
                                    throw new InternalServerError(
                                        JsonUtils.Deserialize<FiveHundred>(responseBody)
                                    );
                            }
                        }
                        catch (JsonException)
                        {
                            // unable to map error response, throwing generic error
                        }
                        throw new PayrocApiException(
                            $"Error with status code {response.StatusCode}",
                            response.StatusCode,
                            responseBody
                        );
                    }
                };
                return await PayrocPagerFactory
                    .CreateAsync<Batch>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

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
    /// <example><code>
    /// await client.Reporting.Settlement.RetrieveBatchAsync(
    ///     new RetrieveBatchSettlementRequest { BatchId = 1 }
    /// );
    /// </code></example>
    public async Task<Batch> RetrieveBatchAsync(
        RetrieveBatchSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "batches/{0}",
                                ValueConvert.ToPathParameterString(request.BatchId)
                            ),
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonUtils.Deserialize<Batch>(responseBody)!;
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocException("Failed to deserialize response", e);
                    }
                }

                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a list of transactions.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.ListTransactionsAsync(
    ///     new ListReportingSettlementTransactionsRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Date = new DateOnly(2024, 7, 1),
    ///         BatchId = 1,
    ///         MerchantId = "4525644354",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<Transaction>> ListTransactionsAsync(
        ListReportingSettlementTransactionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["date"] = request.Date.ToString(Constants.DateFormat);
                _query["batchId"] = request.BatchId.ToString();
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                if (request.MerchantId != null)
                {
                    _query["merchantId"] = request.MerchantId;
                }
                if (request.TransactionType != null)
                {
                    _query["transactionType"] = request.TransactionType.Value.Stringify();
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "transactions",
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
                    }

                    {
                        var responseBody = await response.Raw.Content.ReadAsStringAsync();
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case 400:
                                    throw new BadRequestError(
                                        JsonUtils.Deserialize<FourHundred>(responseBody)
                                    );
                                case 401:
                                    throw new UnauthorizedError(
                                        JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                    );
                                case 403:
                                    throw new ForbiddenError(
                                        JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                    );
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                    );
                                case 500:
                                    throw new InternalServerError(
                                        JsonUtils.Deserialize<FiveHundred>(responseBody)
                                    );
                            }
                        }
                        catch (JsonException)
                        {
                            // unable to map error response, throwing generic error
                        }
                        throw new PayrocApiException(
                            $"Error with status code {response.StatusCode}",
                            response.StatusCode,
                            responseBody
                        );
                    }
                };
                return await PayrocPagerFactory
                    .CreateAsync<Transaction>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a specific transaction.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.RetrieveTransactionAsync(
    ///     new RetrieveTransactionSettlementRequest { TransactionId = 1 }
    /// );
    /// </code></example>
    public async Task<Transaction> RetrieveTransactionAsync(
        RetrieveTransactionSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "transactions/{0}",
                                ValueConvert.ToPathParameterString(request.TransactionId)
                            ),
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonUtils.Deserialize<Transaction>(responseBody)!;
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocException("Failed to deserialize response", e);
                    }
                }

                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a list of authorizations.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.ListAuthorizationsAsync(
    ///     new ListReportingSettlementAuthorizationsRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Date = new DateOnly(2024, 7, 1),
    ///         BatchId = 1,
    ///         MerchantId = "4525644354",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<Authorization>> ListAuthorizationsAsync(
        ListReportingSettlementAuthorizationsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["date"] = request.Date.ToString(Constants.DateFormat);
                _query["batchId"] = request.BatchId.ToString();
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                if (request.MerchantId != null)
                {
                    _query["merchantId"] = request.MerchantId;
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "authorizations",
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
                    }

                    {
                        var responseBody = await response.Raw.Content.ReadAsStringAsync();
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case 400:
                                    throw new BadRequestError(
                                        JsonUtils.Deserialize<FourHundred>(responseBody)
                                    );
                                case 401:
                                    throw new UnauthorizedError(
                                        JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                    );
                                case 403:
                                    throw new ForbiddenError(
                                        JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                    );
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                    );
                                case 500:
                                    throw new InternalServerError(
                                        JsonUtils.Deserialize<FiveHundred>(responseBody)
                                    );
                            }
                        }
                        catch (JsonException)
                        {
                            // unable to map error response, throwing generic error
                        }
                        throw new PayrocApiException(
                            $"Error with status code {response.StatusCode}",
                            response.StatusCode,
                            responseBody
                        );
                    }
                };
                return await PayrocPagerFactory
                    .CreateAsync<Authorization>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a specific authorization.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.RetrieveAuthorizationAsync(
    ///     new RetrieveAuthorizationSettlementRequest { AuthorizationId = 1 }
    /// );
    /// </code></example>
    public async Task<Authorization> RetrieveAuthorizationAsync(
        RetrieveAuthorizationSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "authorizations/{0}",
                                ValueConvert.ToPathParameterString(request.AuthorizationId)
                            ),
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonUtils.Deserialize<Authorization>(responseBody)!;
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocException("Failed to deserialize response", e);
                    }
                }

                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }

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
    /// <example><code>
    /// await client.Reporting.Settlement.ListDisputesAsync(
    ///     new ListReportingSettlementDisputesRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Date = new DateOnly(2024, 7, 2),
    ///         MerchantId = "4525644354",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<Dispute>> ListDisputesAsync(
        ListReportingSettlementDisputesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["date"] = request.Date.ToString(Constants.DateFormat);
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                if (request.MerchantId != null)
                {
                    _query["merchantId"] = request.MerchantId;
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "disputes",
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
                    }

                    {
                        var responseBody = await response.Raw.Content.ReadAsStringAsync();
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case 400:
                                    throw new BadRequestError(
                                        JsonUtils.Deserialize<FourHundred>(responseBody)
                                    );
                                case 401:
                                    throw new UnauthorizedError(
                                        JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                    );
                                case 403:
                                    throw new ForbiddenError(
                                        JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                    );
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                    );
                                case 500:
                                    throw new InternalServerError(
                                        JsonUtils.Deserialize<FiveHundred>(responseBody)
                                    );
                            }
                        }
                        catch (JsonException)
                        {
                            // unable to map error response, throwing generic error
                        }
                        throw new PayrocApiException(
                            $"Error with status code {response.StatusCode}",
                            response.StatusCode,
                            responseBody
                        );
                    }
                };
                return await PayrocPagerFactory
                    .CreateAsync<Dispute>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Use this method to return the status history of a dispute.
    ///
    /// To return the status history of a dispute, you need its disputeId. If you don't have the disputeId, use our [List Disputes](https://docs.payroc.com/api/schema/reporting/settlement/list-disputes) method to search for the dispute.
    ///
    /// Our gateway returns a list that contains each status change, the date it was changed, and its updated status.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.ListDisputesStatusesAsync(
    ///     new ListDisputesStatusesSettlementRequest { DisputeId = 1 }
    /// );
    /// </code></example>
    public async Task<IEnumerable<DisputeStatus>> ListDisputesStatusesAsync(
        ListDisputesStatusesSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "disputes/{0}/statuses",
                                ValueConvert.ToPathParameterString(request.DisputeId)
                            ),
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonUtils.Deserialize<IEnumerable<DisputeStatus>>(responseBody)!;
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocException("Failed to deserialize response", e);
                    }
                }

                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a list of ACH deposits.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.ListAchDepositsAsync(
    ///     new ListReportingSettlementAchDepositsRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Date = new DateOnly(2024, 7, 2),
    ///         MerchantId = "4525644354",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<AchDeposit>> ListAchDepositsAsync(
        ListReportingSettlementAchDepositsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["date"] = request.Date.ToString(Constants.DateFormat);
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                if (request.MerchantId != null)
                {
                    _query["merchantId"] = request.MerchantId;
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "ach-deposits",
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
                    }

                    {
                        var responseBody = await response.Raw.Content.ReadAsStringAsync();
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case 400:
                                    throw new BadRequestError(
                                        JsonUtils.Deserialize<FourHundred>(responseBody)
                                    );
                                case 401:
                                    throw new UnauthorizedError(
                                        JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                    );
                                case 403:
                                    throw new ForbiddenError(
                                        JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                    );
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                    );
                                case 500:
                                    throw new InternalServerError(
                                        JsonUtils.Deserialize<FiveHundred>(responseBody)
                                    );
                            }
                        }
                        catch (JsonException)
                        {
                            // unable to map error response, throwing generic error
                        }
                        throw new PayrocApiException(
                            $"Error with status code {response.StatusCode}",
                            response.StatusCode,
                            responseBody
                        );
                    }
                };
                return await PayrocPagerFactory
                    .CreateAsync<AchDeposit>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a specific ACH deposit.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.RetrieveAchDepositAsync(
    ///     new RetrieveAchDepositSettlementRequest { AchDepositId = 1 }
    /// );
    /// </code></example>
    public async Task<AchDeposit> RetrieveAchDepositAsync(
        RetrieveAchDepositSettlementRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "ach-deposits/{0}",
                                ValueConvert.ToPathParameterString(request.AchDepositId)
                            ),
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonUtils.Deserialize<AchDeposit>(responseBody)!;
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocException("Failed to deserialize response", e);
                    }
                }

                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        switch (response.StatusCode)
                        {
                            case 400:
                                throw new BadRequestError(
                                    JsonUtils.Deserialize<FourHundred>(responseBody)
                                );
                            case 401:
                                throw new UnauthorizedError(
                                    JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                );
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 406:
                                throw new NotAcceptableError(
                                    JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                );
                            case 500:
                                throw new InternalServerError(
                                    JsonUtils.Deserialize<FiveHundred>(responseBody)
                                );
                        }
                    }
                    catch (JsonException)
                    {
                        // unable to map error response, throwing generic error
                    }
                    throw new PayrocApiException(
                        $"Error with status code {response.StatusCode}",
                        response.StatusCode,
                        responseBody
                    );
                }
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve a list of ACH deposit fees.
    /// </summary>
    /// <example><code>
    /// await client.Reporting.Settlement.ListAchDepositFeesAsync(
    ///     new ListReportingSettlementAchDepositFeesRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Date = new DateOnly(2024, 7, 2),
    ///         AchDepositId = 1,
    ///         MerchantId = "4525644354",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<AchDepositFee>> ListAchDepositFeesAsync(
        ListReportingSettlementAchDepositFeesRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["date"] = request.Date.ToString(Constants.DateFormat);
                _query["achDepositId"] = request.AchDepositId.ToString();
                if (request.Before != null)
                {
                    _query["before"] = request.Before;
                }
                if (request.After != null)
                {
                    _query["after"] = request.After;
                }
                if (request.Limit != null)
                {
                    _query["limit"] = request.Limit.Value.ToString();
                }
                if (request.MerchantId != null)
                {
                    _query["merchantId"] = request.MerchantId;
                }
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = "ach-deposit-fees",
                        Query = _query,
                        Options = options,
                    }
                );
                var sendRequest = async (
                    HttpRequestMessage httpRequest,
                    CancellationToken cancellationToken
                ) =>
                {
                    var response = await _client
                        .SendRequestAsync(httpRequest, options, cancellationToken)
                        .ConfigureAwait(false);
                    if (response.StatusCode is >= 200 and < 400)
                    {
                        return response.Raw;
                    }

                    {
                        var responseBody = await response.Raw.Content.ReadAsStringAsync();
                        try
                        {
                            switch (response.StatusCode)
                            {
                                case 400:
                                    throw new BadRequestError(
                                        JsonUtils.Deserialize<FourHundred>(responseBody)
                                    );
                                case 401:
                                    throw new UnauthorizedError(
                                        JsonUtils.Deserialize<FourHundredOne>(responseBody)
                                    );
                                case 403:
                                    throw new ForbiddenError(
                                        JsonUtils.Deserialize<FourHundredThree>(responseBody)
                                    );
                                case 406:
                                    throw new NotAcceptableError(
                                        JsonUtils.Deserialize<FourHundredSix>(responseBody)
                                    );
                                case 500:
                                    throw new InternalServerError(
                                        JsonUtils.Deserialize<FiveHundred>(responseBody)
                                    );
                            }
                        }
                        catch (JsonException)
                        {
                            // unable to map error response, throwing generic error
                        }
                        throw new PayrocApiException(
                            $"Error with status code {response.StatusCode}",
                            response.StatusCode,
                            responseBody
                        );
                    }
                };
                return await PayrocPagerFactory
                    .CreateAsync<AchDepositFee>(
                        new PayrocPagerContext()
                        {
                            SendRequest = sendRequest,
                            InitialHttpRequest = httpRequest,
                            ClientOptions = _client.Options,
                            RequestOptions = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }
}
