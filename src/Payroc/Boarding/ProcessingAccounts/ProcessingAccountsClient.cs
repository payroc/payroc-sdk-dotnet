using System.Text.Json;
using OneOf;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

public partial class ProcessingAccountsClient : IProcessingAccountsClient
{
    private RawClient _client;

    internal ProcessingAccountsClient(RawClient client)
    {
        try
        {
            _client = client;
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    private async Task<WithRawResponse<ProcessingAccount>> RetrieveAsyncCore(
        RetrieveProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "processing-accounts/{0}",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            Headers = _headers,
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
                        var responseData = JsonUtils.Deserialize<ProcessingAccount>(responseBody)!;
                        return new WithRawResponse<ProcessingAccount>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
                                    JsonUtils.Deserialize<object>(responseBody)
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

    private async Task<
        WithRawResponse<IEnumerable<FundingAccount>>
    > ListProcessingAccountFundingAccountsAsyncCore(
        ListProcessingAccountFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "processing-accounts/{0}/funding-accounts",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            Headers = _headers,
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
                        var responseData = JsonUtils.Deserialize<IEnumerable<FundingAccount>>(
                            responseBody
                        )!;
                        return new WithRawResponse<IEnumerable<FundingAccount>>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
                                    JsonUtils.Deserialize<object>(responseBody)
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

    private async Task<WithRawResponse<PaginatedContacts>> ListContactsAsyncCore(
        ListContactsProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 3)
                    .Add("before", request.Before)
                    .Add("after", request.After)
                    .Add("limit", request.Limit)
                    .MergeAdditional(options?.AdditionalQueryParameters)
                    .Build();
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "processing-accounts/{0}/contacts",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            QueryString = _queryString,
                            Headers = _headers,
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
                        var responseData = JsonUtils.Deserialize<PaginatedContacts>(responseBody)!;
                        return new WithRawResponse<PaginatedContacts>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
                                    JsonUtils.Deserialize<object>(responseBody)
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

    private async Task<
        WithRawResponse<OneOf<PricingAgreementUs40, PricingAgreementUs50>>
    > GetProcessingAccountPricingAgreementAsyncCore(
        GetProcessingAccountPricingAgreementProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "processing-accounts/{0}/pricing",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            Headers = _headers,
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
                        var responseData = JsonUtils.Deserialize<
                            OneOf<PricingAgreementUs40, PricingAgreementUs50>
                        >(responseBody)!;
                        return new WithRawResponse<
                            OneOf<PricingAgreementUs40, PricingAgreementUs50>
                        >()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
                                    JsonUtils.Deserialize<object>(responseBody)
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

    private async Task<
        WithRawResponse<CreateReminderProcessingAccountsResponse>
    > CreateReminderAsyncCore(
        CreateReminderProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add("Idempotency-Key", request.IdempotencyKey)
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Post,
                            Path = string.Format(
                                "processing-accounts/{0}/reminders",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            Body = request.Body,
                            Headers = _headers,
                            ContentType = "application/json",
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
                        var responseData =
                            JsonUtils.Deserialize<CreateReminderProcessingAccountsResponse>(
                                responseBody
                            )!;
                        return new WithRawResponse<CreateReminderProcessingAccountsResponse>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<object>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 409:
                                throw new ConflictError(
                                    JsonUtils.Deserialize<FourHundredNine>(responseBody)
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

    private async Task<WithRawResponse<IEnumerable<TerminalOrder>>> ListTerminalOrdersAsyncCore(
        ListTerminalOrdersProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 3)
                    .Add("status", request.Status)
                    .Add("fromDateTime", request.FromDateTime)
                    .Add("toDateTime", request.ToDateTime)
                    .MergeAdditional(options?.AdditionalQueryParameters)
                    .Build();
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Get,
                            Path = string.Format(
                                "processing-accounts/{0}/terminal-orders",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            QueryString = _queryString,
                            Headers = _headers,
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
                        var responseData = JsonUtils.Deserialize<IEnumerable<TerminalOrder>>(
                            responseBody
                        )!;
                        return new WithRawResponse<IEnumerable<TerminalOrder>>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
                                    JsonUtils.Deserialize<object>(responseBody)
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

    private async Task<WithRawResponse<TerminalOrder>> CreateTerminalOrderAsyncCore(
        CreateTerminalOrder request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add("Idempotency-Key", request.IdempotencyKey)
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var response = await _client
                    .SendRequestAsync(
                        new JsonRequest
                        {
                            BaseUrl = _client.Options.Environment.Api,
                            Method = HttpMethod.Post,
                            Path = string.Format(
                                "processing-accounts/{0}/terminal-orders",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            Body = request,
                            Headers = _headers,
                            ContentType = "application/json",
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
                        var responseData = JsonUtils.Deserialize<TerminalOrder>(responseBody)!;
                        return new WithRawResponse<TerminalOrder>()
                        {
                            Data = responseData,
                            RawResponse = new RawResponse()
                            {
                                StatusCode = response.Raw.StatusCode,
                                Url =
                                    response.Raw.RequestMessage?.RequestUri
                                    ?? new Uri("about:blank"),
                                Headers = ResponseHeaders.FromHttpResponseMessage(response.Raw),
                            },
                        };
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocApiException(
                            "Failed to deserialize response",
                            response.StatusCode,
                            responseBody,
                            e
                        );
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
                                    JsonUtils.Deserialize<object>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
                                );
                            case 409:
                                throw new ConflictError(
                                    JsonUtils.Deserialize<FourHundredNine>(responseBody)
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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.RetrieveAsync(
    ///     new RetrieveProcessingAccountsRequest { ProcessingAccountId = "38765" }
    /// );
    /// </code></example>
    public WithRawResponseTask<ProcessingAccount> RetrieveAsync(
        RetrieveProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ProcessingAccount>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListProcessingAccountFundingAccountsAsync(
    ///     new ListProcessingAccountFundingAccountsRequest { ProcessingAccountId = "38765" }
    /// );
    /// </code></example>
    public WithRawResponseTask<
        IEnumerable<FundingAccount>
    > ListProcessingAccountFundingAccountsAsync(
        ListProcessingAccountFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<IEnumerable<FundingAccount>>(
            ListProcessingAccountFundingAccountsAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListContactsAsync(
    ///     new ListContactsProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<PaginatedContacts> ListContactsAsync(
        ListContactsProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<PaginatedContacts>(
            ListContactsAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.GetProcessingAccountPricingAgreementAsync(
    ///     new GetProcessingAccountPricingAgreementProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<
        OneOf<PricingAgreementUs40, PricingAgreementUs50>
    > GetProcessingAccountPricingAgreementAsync(
        GetProcessingAccountPricingAgreementProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<OneOf<PricingAgreementUs40, PricingAgreementUs50>>(
            GetProcessingAccountPricingAgreementAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListOwnersAsync(
    ///     new ListProcessingAccountOwnersRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<Owner>> ListOwnersAsync(
        ListProcessingAccountOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 3)
                    .Add("before", request.Before)
                    .Add("after", request.After)
                    .Add("limit", request.Limit)
                    .MergeAdditional(options?.AdditionalQueryParameters)
                    .Build();
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var httpRequest = await _client.CreateHttpRequestAsync(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = string.Format(
                            "processing-accounts/{0}/owners",
                            ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                        ),
                        QueryString = _queryString,
                        Headers = _headers,
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
                };
                return await PayrocPagerFactory
                    .CreateAsync<Owner>(
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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.CreateReminderAsync(
    ///     new CreateReminderProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new CreateReminderProcessingAccountsRequestBody(
    ///             new CreateReminderProcessingAccountsRequestBody.PricingAgreement(
    ///                 new PricingAgreementReminder()
    ///             )
    ///         ),
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<CreateReminderProcessingAccountsResponse> CreateReminderAsync(
        CreateReminderProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<CreateReminderProcessingAccountsResponse>(
            CreateReminderAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListTerminalOrdersAsync(
    ///     new ListTerminalOrdersProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         Status = ListTerminalOrdersProcessingAccountsRequestStatus.Open,
    ///         FromDateTime = new DateTime(2024, 09, 08, 12, 00, 00, 000),
    ///         ToDateTime = new DateTime(2024, 12, 08, 11, 00, 00, 000),
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<IEnumerable<TerminalOrder>> ListTerminalOrdersAsync(
        ListTerminalOrdersProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<IEnumerable<TerminalOrder>>(
            ListTerminalOrdersAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.CreateTerminalOrderAsync(
    ///     new CreateTerminalOrder
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         TrainingProvider = TrainingProvider.Payroc,
    ///         Shipping = new CreateTerminalOrderShipping
    ///         {
    ///             Preferences = new CreateTerminalOrderShippingPreferences
    ///             {
    ///                 Method = CreateTerminalOrderShippingPreferencesMethod.NextDay,
    ///                 SaturdayDelivery = true,
    ///             },
    ///             Address = new CreateTerminalOrderShippingAddress
    ///             {
    ///                 RecipientName = "Recipient Name",
    ///                 BusinessName = "Company Ltd",
    ///                 AddressLine1 = "1 Example Ave.",
    ///                 AddressLine2 = "Example Address Line 2",
    ///                 City = "Chicago",
    ///                 State = "Illinois",
    ///                 PostalCode = "60056",
    ///                 Email = "example@mail.com",
    ///                 Phone = "2025550164",
    ///             },
    ///         },
    ///         OrderItems = new List&lt;OrderItem&gt;()
    ///         {
    ///             new OrderItem
    ///             {
    ///                 Type = OrderItemType.Solution,
    ///                 SolutionTemplateId = "Roc Services_DX8000",
    ///                 SolutionQuantity = 1,
    ///                 DeviceCondition = OrderItemDeviceCondition.New,
    ///                 SolutionSetup = new OrderItemSolutionSetup
    ///                 {
    ///                     Timezone = SchemasTimezone.AmericaChicago,
    ///                     IndustryTemplateId = "Retail",
    ///                     GatewaySettings = new OrderItemSolutionSetupGatewaySettings
    ///                     {
    ///                         MerchantPortfolioId = "Company Ltd",
    ///                         MerchantTemplateId = "Company Ltd Merchant Template",
    ///                         UserTemplateId = "Company Ltd User Template",
    ///                         TerminalTemplateId = "Company Ltd Terminal Template",
    ///                     },
    ///                     ApplicationSettings = new OrderItemSolutionSetupApplicationSettings
    ///                     {
    ///                         ClerkPrompt = false,
    ///                         Security = new OrderItemSolutionSetupApplicationSettingsSecurity
    ///                         {
    ///                             RefundPassword = true,
    ///                             KeyedSalePassword = false,
    ///                             ReversalPassword = true,
    ///                         },
    ///                     },
    ///                     DeviceSettings = new OrderItemSolutionSetupDeviceSettings
    ///                     {
    ///                         NumberOfMobileUsers = 2,
    ///                         CommunicationType =
    ///                             OrderItemSolutionSetupDeviceSettingsCommunicationType.Wifi,
    ///                     },
    ///                     BatchClosure = new OrderItemSolutionSetupBatchClosure(
    ///                         new OrderItemSolutionSetupBatchClosure.Automatic(new AutomaticBatchClose())
    ///                     ),
    ///                     ReceiptNotifications = new OrderItemSolutionSetupReceiptNotifications
    ///                     {
    ///                         EmailReceipt = true,
    ///                         SmsReceipt = false,
    ///                     },
    ///                     Taxes = new List&lt;OrderItemSolutionSetupTaxesItem&gt;()
    ///                     {
    ///                         new OrderItemSolutionSetupTaxesItem
    ///                         {
    ///                             TaxRate = 6f,
    ///                             TaxLabel = "Sales Tax",
    ///                         },
    ///                     },
    ///                     Tips = new OrderItemSolutionSetupTips { Enabled = false },
    ///                     Tokenization = true,
    ///                 },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<TerminalOrder> CreateTerminalOrderAsync(
        CreateTerminalOrder request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<TerminalOrder>(
            CreateTerminalOrderAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListProcessingTerminalsAsync(
    ///     new ListProcessingTerminalsProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<ProcessingTerminal>> ListProcessingTerminalsAsync(
        ListProcessingTerminalsProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 3)
                    .Add("before", request.Before)
                    .Add("after", request.After)
                    .Add("limit", request.Limit)
                    .MergeAdditional(options?.AdditionalQueryParameters)
                    .Build();
                var _headers = await new Payroc.Core.HeadersBuilder.Builder()
                    .Add(_client.Options.Headers)
                    .Add(_client.Options.AdditionalHeaders)
                    .Add(options?.AdditionalHeaders)
                    .BuildAsync()
                    .ConfigureAwait(false);
                var httpRequest = await _client.CreateHttpRequestAsync(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = string.Format(
                            "processing-accounts/{0}/processing-terminals",
                            ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                        ),
                        QueryString = _queryString,
                        Headers = _headers,
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
                                        JsonUtils.Deserialize<object>(responseBody)
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
                };
                return await PayrocPagerFactory
                    .CreateAsync<ProcessingTerminal>(
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
