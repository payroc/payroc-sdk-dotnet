using System.Net.Http;
using System.Text.Json;
using System.Threading;
using OneOf;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

public partial class ProcessingAccountsClient
{
    private RawClient _client;

    internal ProcessingAccountsClient(RawClient client)
    {
        _client = client;
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
    ///     new RetrieveProcessingAccountsRequest { ProcessingAccountId = "processingAccountId" }
    /// );
    /// </code></example>
    public async Task<ProcessingAccount> RetrieveAsync(
        RetrieveProcessingAccountsRequest request,
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
                                "processing-accounts/{0}",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
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
                        return JsonUtils.Deserialize<ProcessingAccount>(responseBody)!;
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

    /// <summary>
    /// Retrieve a list of funding accounts associated with a processing account.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListProcessingAccountFundingAccountsAsync(
    ///     new ListProcessingAccountFundingAccountsRequest { ProcessingAccountId = "processingAccountId" }
    /// );
    /// </code></example>
    public async Task<IEnumerable<FundingAccount>> ListProcessingAccountFundingAccountsAsync(
        ListProcessingAccountFundingAccountsRequest request,
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
                                "processing-accounts/{0}/funding-accounts",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
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
                        return JsonUtils.Deserialize<IEnumerable<FundingAccount>>(responseBody)!;
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

    /// <summary>
    /// Use this method to retrieve a list of contacts for a processing account.
    ///
    /// **Note:** If you want to view a specific contact and you have their contactId, go to our [Retrieve Contact](https://docs.payroc.com/api/schema/boarding/contacts/retrieve) method.
    ///
    /// To retrieve a list of contacts for a processing account, you need the processingAccountId. Our gateway returned the processingAccountId in the response of the [Create Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create) method or the [Create Processing Account](https://docs.payroc.com/api/schema/boarding/merchant-platforms/create-processing-account) method.
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
    ///     new ListContactsProcessingAccountsRequest { ProcessingAccountId = "processingAccountId" }
    /// );
    /// </code></example>
    public async Task<PaginatedContacts> ListContactsAsync(
        ListContactsProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
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
                            Query = _query,
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
                        return JsonUtils.Deserialize<PaginatedContacts>(responseBody)!;
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
    ///         ProcessingAccountId = "processingAccountId",
    ///     }
    /// );
    /// </code></example>
    public async Task<
        OneOf<PricingAgreementUs40, PricingAgreementUs50>
    > GetProcessingAccountPricingAgreementAsync(
        GetProcessingAccountPricingAgreementProcessingAccountsRequest request,
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
                                "processing-accounts/{0}/pricing",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
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
                        return JsonUtils.Deserialize<
                            OneOf<PricingAgreementUs40, PricingAgreementUs50>
                        >(responseBody)!;
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

    /// <summary>
    /// Retrieve owners associated with a processing account.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListOwnersAsync(
    ///     new ListProcessingAccountOwnersRequest { ProcessingAccountId = "processingAccountId" }
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
                var _query = new Dictionary<string, object>();
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
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = string.Format(
                            "processing-accounts/{0}/owners",
                            ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                        ),
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
    ///         ProcessingAccountId = "processingAccountId",
    ///         Body = new CreateReminderProcessingAccountsRequestBody(
    ///             new CreateReminderProcessingAccountsRequestBody.PricingAgreement(
    ///                 new PricingAgreementReminder()
    ///             )
    ///         ),
    ///     }
    /// );
    /// </code></example>
    public async Task<CreateReminderProcessingAccountsResponse> CreateReminderAsync(
        CreateReminderProcessingAccountsRequest request,
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
                            Method = HttpMethod.Post,
                            Path = string.Format(
                                "processing-accounts/{0}/reminders",
                                ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                            ),
                            Body = request.Body,
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
                        return JsonUtils.Deserialize<CreateReminderProcessingAccountsResponse>(
                            responseBody
                        )!;
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
                            case 403:
                                throw new ForbiddenError(
                                    JsonUtils.Deserialize<object>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
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
    /// Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of terminal orders associated with a processing account.
    ///
    /// **Note:** If you want to view a specific terminal order and you have its terminalOrderId, use our [Retrieve Terminal Order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve) method.
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
    ///         ProcessingAccountId = "processingAccountId",
    ///         FromDateTime = new DateTime(2024, 09, 08, 12, 00, 00, 000),
    ///         ToDateTime = new DateTime(2024, 12, 08, 11, 00, 00, 000),
    ///     }
    /// );
    /// </code></example>
    public async Task<IEnumerable<TerminalOrder>> ListTerminalOrdersAsync(
        ListTerminalOrdersProcessingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                if (request.Status != null)
                {
                    _query["status"] = request.Status.Value.Stringify();
                }
                if (request.FromDateTime != null)
                {
                    _query["fromDateTime"] = request.FromDateTime.Value.ToString(
                        Constants.DateTimeFormat
                    );
                }
                if (request.ToDateTime != null)
                {
                    _query["toDateTime"] = request.ToDateTime.Value.ToString(
                        Constants.DateTimeFormat
                    );
                }
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
                            Query = _query,
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
                        return JsonUtils.Deserialize<IEnumerable<TerminalOrder>>(responseBody)!;
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

    /// <summary>
    /// Use this method to order and configure terminals for a processing account.
    ///
    /// **Note**: You need the ID of the processing account before you can create an order. If you don't know the processingAccountId, go to the [Retrieve a Merchant Platform](https://docs.payroc.com/api/schema/boarding/merchant-platforms/retrieve) method.
    ///
    /// In the request, specify the gateway settings, device settings, and application settings for the terminal.
    ///
    /// In the response, our gateway returns information about the terminal order including its status and terminalOrderId that you can use to [retrieve the terminal order](https://docs.payroc.com/api/schema/boarding/terminal-orders/retrieve).
    ///
    /// **Note**: You can subscribe to the terminalOrder.status.changed event to get notifications when we update the status of a terminal order. For more information about how to subscribe to events, go to [Events Subscriptions](https://docs.payroc.com/guides/integrate/event-subscriptions).
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.CreateTerminalOrderAsync(
    ///     new CreateTerminalOrder
    ///     {
    ///         ProcessingAccountId = "processingAccountId",
    ///         IdempotencyKey = "Idempotency-Key",
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
    ///                 Type = "solution",
    ///                 SolutionTemplateId = "Roc Services_DX8000",
    ///                 SolutionQuantity = 1f,
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
    ///                         NumberOfMobileUsers = 2f,
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
    public async Task<TerminalOrder> CreateTerminalOrderAsync(
        CreateTerminalOrder request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _headers = new Headers(
                    new Dictionary<string, string>()
                    {
                        { "Idempotency-Key", request.IdempotencyKey },
                    }
                );
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
                        return JsonUtils.Deserialize<TerminalOrder>(responseBody)!;
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
                                    JsonUtils.Deserialize<object>(responseBody)
                                );
                            case 404:
                                throw new NotFoundError(
                                    JsonUtils.Deserialize<FourHundredFour>(responseBody)
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
    /// Use this method to retrieve a [paginated](https://docs.payroc.com/api/pagination) list of processing terminals associated with a processing account.
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
    ///         ProcessingAccountId = "processingAccountId",
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
                var _query = new Dictionary<string, object>();
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
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = string.Format(
                            "processing-accounts/{0}/processing-terminals",
                            ValueConvert.ToPathParameterString(request.ProcessingAccountId)
                        ),
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
