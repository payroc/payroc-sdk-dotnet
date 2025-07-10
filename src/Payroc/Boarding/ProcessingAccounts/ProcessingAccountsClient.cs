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
    /// Retrieve a specific processing account.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.RetrieveAsync(
    ///     new RetrieveProcessingAccountsRequest { ProcessingAccountId = "38765" }
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
    ///     new ListProcessingAccountFundingAccountsRequest { ProcessingAccountId = "38765" }
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
    /// Retrieve a list of contacts associated with a processing account.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListContactsAsync(
    ///     new ListContactsProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         Before = "2571",
    ///         After = "8516",
    ///     }
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
    /// Retrieve a pricing agreement for a processing account.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.GetProcessingAccountPricingAgreementAsync(
    ///     new GetProcessingAccountPricingAgreementProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
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
    ///     new ListProcessingAccountOwnersRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         Before = "2571",
    ///         After = "8516",
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
    /// When you create a processing account, we send a copy of the pricing agreement to the merchant to sign. You can choose to send them a copy of the pricing agreement by email, or you can generate a link to the pricing agreement.&lt;br/&gt;
    /// If you requested the merchant's signature by email and they don't respond, use our Reminders endpoint to create a reminder and to send another email.&lt;br/&gt;
    /// **Note:** You can use the Reminders endpoint only if you request the merchant's signature by email. If you generate a link to the pricing agreement, you can't use the Reminders endpoint.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.CreateReminderAsync(
    ///     new CreateReminderProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
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
    /// Use this method to retrieve a list of terminal orders associated with a processing account.
    /// Send the processingAccountId in the path parameter of your request.
    /// &gt; **Note**: If you don't know the processingAccountId, go to [List merchant platform's processing accounts](#listMerchantLocations).
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListTerminalOrdersAsync(
    ///     new ListTerminalOrdersProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
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
    /// Use this method to order and configure terminals for a processing account. When you create an order, you specify the gateway settings, device settings, and application settings for the terminals.
    /// **Note**: You need the ID of the merchant's processing account before you can create an order. If you don't know the processingAccountId, go to [Retrieve a Merchant Platform.](#getMerchantAcccounts)
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
    /// Use this method to retrieve a [paginated](/api/pagination) list of processing terminals associated with a processing account.
    /// Send the processingAccountId in the path parameter of your request.
    /// &gt; **Note**: If you don't know the processingAccountId, go to [List merchant platform's processing accounts](#listMerchantLocations).
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingAccounts.ListProcessingTerminalsAsync(
    ///     new ListProcessingTerminalsProcessingAccountsRequest
    ///     {
    ///         ProcessingAccountId = "38765",
    ///         Before = "2571",
    ///         After = "8516",
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
