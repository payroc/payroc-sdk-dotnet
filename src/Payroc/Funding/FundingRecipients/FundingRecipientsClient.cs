using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

public partial class FundingRecipientsClient : IFundingRecipientsClient
{
    private RawClient _client;

    internal FundingRecipientsClient(RawClient client)
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

    private async Task<WithRawResponse<FundingRecipient>> CreateAsyncCore(
        CreateFundingRecipient request,
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
                            Path = "funding-recipients",
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
                        var responseData = JsonUtils.Deserialize<FundingRecipient>(responseBody)!;
                        return new WithRawResponse<FundingRecipient>()
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

    private async Task<WithRawResponse<FundingRecipient>> RetrieveAsyncCore(
        RetrieveFundingRecipientsRequest request,
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
                                "funding-recipients/{0}",
                                ValueConvert.ToPathParameterString(request.RecipientId)
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
                        var responseData = JsonUtils.Deserialize<FundingRecipient>(responseBody)!;
                        return new WithRawResponse<FundingRecipient>()
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

    private async Task<WithRawResponse<IEnumerable<FundingAccount>>> ListAccountsAsyncCore(
        ListFundingRecipientFundingAccountsRequest request,
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
                                "funding-recipients/{0}/funding-accounts",
                                ValueConvert.ToPathParameterString(request.RecipientId)
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

    private async Task<WithRawResponse<FundingAccount>> CreateAccountAsyncCore(
        CreateAccountFundingRecipientsRequest request,
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
                                "funding-recipients/{0}/funding-accounts",
                                ValueConvert.ToPathParameterString(request.RecipientId)
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
                        var responseData = JsonUtils.Deserialize<FundingAccount>(responseBody)!;
                        return new WithRawResponse<FundingAccount>()
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

    private async Task<WithRawResponse<IEnumerable<Owner>>> ListOwnersAsyncCore(
        ListFundingRecipientOwnersRequest request,
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
                                "funding-recipients/{0}/owners",
                                ValueConvert.ToPathParameterString(request.RecipientId)
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
                        var responseData = JsonUtils.Deserialize<IEnumerable<Owner>>(responseBody)!;
                        return new WithRawResponse<IEnumerable<Owner>>()
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

    private async Task<WithRawResponse<Owner>> CreateOwnerAsyncCore(
        CreateOwnerFundingRecipientsRequest request,
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
                                "funding-recipients/{0}/owners",
                                ValueConvert.ToPathParameterString(request.RecipientId)
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
                        var responseData = JsonUtils.Deserialize<Owner>(responseBody)!;
                        return new WithRawResponse<Owner>()
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
    /// <example><code>
    /// await client.Funding.FundingRecipients.ListAsync(
    ///     new ListFundingRecipientsRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<FundingRecipient>> ListAsync(
        ListFundingRecipientsRequest request,
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
                        Path = "funding-recipients",
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
                    .CreateAsync<FundingRecipient>(
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
    /// <example><code>
    /// await client.Funding.FundingRecipients.CreateAsync(
    ///     new CreateFundingRecipient
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         RecipientType = CreateFundingRecipientRecipientType.PrivateCorporation,
    ///         TaxId = "12-3456789",
    ///         DoingBusinessAs = "Pizza Doe",
    ///         Address = new Address
    ///         {
    ///             Address1 = "1 Example Ave.",
    ///             Address2 = "Example Address Line 2",
    ///             Address3 = "Example Address Line 3",
    ///             City = "Chicago",
    ///             State = "Illinois",
    ///             Country = "US",
    ///             PostalCode = "60056",
    ///         },
    ///         ContactMethods = new List&lt;ContactMethod&gt;()
    ///         {
    ///             new ContactMethod(
    ///                 new ContactMethod.Email(new ContactMethodEmail { Value = "jane.doe@example.com" })
    ///             ),
    ///             new ContactMethod(
    ///                 new ContactMethod.Phone(new ContactMethodPhone { Value = "2025550164" })
    ///             ),
    ///         },
    ///         Metadata = new Dictionary&lt;string, string&gt;() { { "yourCustomField", "abc123" } },
    ///         Owners = new List&lt;Owner&gt;()
    ///         {
    ///             new Owner
    ///             {
    ///                 FirstName = "Jane",
    ///                 MiddleName = "Helen",
    ///                 LastName = "Doe",
    ///                 DateOfBirth = new DateOnly(1964, 3, 22),
    ///                 Address = new Address
    ///                 {
    ///                     Address1 = "1 Example Ave.",
    ///                     City = "Chicago",
    ///                     State = "Illinois",
    ///                     Country = "US",
    ///                     PostalCode = "60056",
    ///                 },
    ///                 Identifiers = new List&lt;Identifier&gt;()
    ///                 {
    ///                     new Identifier { Type = IdentifierType.NationalId, Value = "000-00-4320" },
    ///                 },
    ///                 ContactMethods = new List&lt;ContactMethod&gt;()
    ///                 {
    ///                     new ContactMethod(
    ///                         new ContactMethod.Email(
    ///                             new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                         )
    ///                     ),
    ///                     new ContactMethod(
    ///                         new ContactMethod.Phone(new ContactMethodPhone { Value = "2025550164" })
    ///                     ),
    ///                 },
    ///                 Relationship = new OwnerRelationship
    ///                 {
    ///                     EquityPercentage = 48.5f,
    ///                     Title = "CFO",
    ///                     IsControlProng = true,
    ///                     IsAuthorizedSignatory = false,
    ///                 },
    ///             },
    ///         },
    ///         FundingAccounts = new List&lt;FundingAccount&gt;()
    ///         {
    ///             new FundingAccount
    ///             {
    ///                 Type = FundingAccountType.Checking,
    ///                 Use = FundingAccountUse.Credit,
    ///                 NameOnAccount = "Jane Doe",
    ///                 PaymentMethods = new List&lt;PaymentMethodsItem&gt;()
    ///                 {
    ///                     new PaymentMethodsItem(new PaymentMethodsItem.Ach(new PaymentMethodAch())),
    ///                 },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<FundingRecipient> CreateAsync(
        CreateFundingRecipient request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<FundingRecipient>(
            CreateAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Funding.FundingRecipients.RetrieveAsync(
    ///     new RetrieveFundingRecipientsRequest { RecipientId = 1 }
    /// );
    /// </code></example>
    public WithRawResponseTask<FundingRecipient> RetrieveAsync(
        RetrieveFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<FundingRecipient>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Funding.FundingRecipients.UpdateAsync(
    ///     new UpdateFundingRecipientsRequest
    ///     {
    ///         RecipientId = 1,
    ///         Body = new FundingRecipient
    ///         {
    ///             RecipientType = FundingRecipientRecipientType.PrivateCorporation,
    ///             TaxId = "12-3456789",
    ///             DoingBusinessAs = "Doe Hot Dogs",
    ///             Address = new Address
    ///             {
    ///                 Address1 = "2 Example Ave.",
    ///                 Address2 = "Example Address Line 2",
    ///                 Address3 = "Example Address Line 3",
    ///                 City = "Chicago",
    ///                 State = "Illinois",
    ///                 Country = "US",
    ///                 PostalCode = "60056",
    ///             },
    ///             ContactMethods = new List&lt;ContactMethod&gt;()
    ///             {
    ///                 new ContactMethod(
    ///                     new ContactMethod.Email(
    ///                         new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                     )
    ///                 ),
    ///                 new ContactMethod(
    ///                     new ContactMethod.Phone(new ContactMethodPhone { Value = "2025550164" })
    ///                 ),
    ///             },
    ///             Metadata = new Dictionary&lt;string, string&gt;() { { "responsiblePerson", "Jane Doe" } },
    ///             Owners = new List&lt;FundingRecipientOwnersItem&gt;()
    ///             {
    ///                 new FundingRecipientOwnersItem
    ///                 {
    ///                     OwnerId = 12346,
    ///                     Link = new FundingRecipientOwnersItemLink
    ///                     {
    ///                         Rel = "owner",
    ///                         Href = "https://api.payroc.com/v1/owners/12346",
    ///                         Method = "get",
    ///                     },
    ///                 },
    ///             },
    ///             FundingAccounts = new List&lt;FundingRecipientFundingAccountsItem&gt;()
    ///             {
    ///                 new FundingRecipientFundingAccountsItem
    ///                 {
    ///                     FundingAccountId = 124,
    ///                     Status = FundingRecipientFundingAccountsItemStatus.Approved,
    ///                     Link = new FundingRecipientFundingAccountsItemLink
    ///                     {
    ///                         Rel = "fundingAccount",
    ///                         Href = "https://api.payroc.com/v1/funding-accounts/124",
    ///                         Method = "get",
    ///                     },
    ///                 },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task UpdateAsync(
        UpdateFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
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
                            Method = HttpMethod.Put,
                            Path = string.Format(
                                "funding-recipients/{0}",
                                ValueConvert.ToPathParameterString(request.RecipientId)
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
                    return;
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
    /// Use this method to delete a funding recipient, including its funding accounts and owners.
    ///
    /// To delete a funding recipient, you need its recipientId. Our gateway returned the recipientId in the response of the [Create Funding Recipient](https://docs.payroc.com/api/schema/funding/funding-recipients/create) method.
    ///
    /// **Note**: If you don't have the recipientId, use our [List Funding Recipients](https://docs.payroc.com/api/schema/funding/funding-recipients/list) method to search for the funding recipient.
    /// </summary>
    /// <example><code>
    /// await client.Funding.FundingRecipients.DeleteAsync(
    ///     new DeleteFundingRecipientsRequest { RecipientId = 1 }
    /// );
    /// </code></example>
    public async Task DeleteAsync(
        DeleteFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
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
                            Method = HttpMethod.Delete,
                            Path = string.Format(
                                "funding-recipients/{0}",
                                ValueConvert.ToPathParameterString(request.RecipientId)
                            ),
                            Headers = _headers,
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    return;
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
    /// <example><code>
    /// await client.Funding.FundingRecipients.ListAccountsAsync(
    ///     new ListFundingRecipientFundingAccountsRequest { RecipientId = 1 }
    /// );
    /// </code></example>
    public WithRawResponseTask<IEnumerable<FundingAccount>> ListAccountsAsync(
        ListFundingRecipientFundingAccountsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<IEnumerable<FundingAccount>>(
            ListAccountsAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Funding.FundingRecipients.CreateAccountAsync(
    ///     new CreateAccountFundingRecipientsRequest
    ///     {
    ///         RecipientId = 1,
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new FundingAccount
    ///         {
    ///             Type = FundingAccountType.Savings,
    ///             Use = FundingAccountUse.Credit,
    ///             NameOnAccount = "Fred Nerk",
    ///             PaymentMethods = new List&lt;PaymentMethodsItem&gt;()
    ///             {
    ///                 new PaymentMethodsItem(new PaymentMethodsItem.Ach(new PaymentMethodAch())),
    ///             },
    ///             Metadata = new Dictionary&lt;string, string&gt;() { { "responsiblePerson", "Jane Doe" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<FundingAccount> CreateAccountAsync(
        CreateAccountFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<FundingAccount>(
            CreateAccountAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Funding.FundingRecipients.ListOwnersAsync(
    ///     new ListFundingRecipientOwnersRequest { RecipientId = 1 }
    /// );
    /// </code></example>
    public WithRawResponseTask<IEnumerable<Owner>> ListOwnersAsync(
        ListFundingRecipientOwnersRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<IEnumerable<Owner>>(
            ListOwnersAsyncCore(request, options, cancellationToken)
        );
    }

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
    /// <example><code>
    /// await client.Funding.FundingRecipients.CreateOwnerAsync(
    ///     new CreateOwnerFundingRecipientsRequest
    ///     {
    ///         RecipientId = 1,
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new Owner
    ///         {
    ///             FirstName = "Fred",
    ///             MiddleName = "Jim",
    ///             LastName = "Nerk",
    ///             DateOfBirth = new DateOnly(1980, 1, 19),
    ///             Address = new Address
    ///             {
    ///                 Address1 = "2 Example Ave.",
    ///                 City = "Chicago",
    ///                 State = "Illinois",
    ///                 Country = "US",
    ///                 PostalCode = "60056",
    ///             },
    ///             Identifiers = new List&lt;Identifier&gt;()
    ///             {
    ///                 new Identifier { Type = IdentifierType.NationalId, Value = "000-00-9876" },
    ///             },
    ///             ContactMethods = new List&lt;ContactMethod&gt;()
    ///             {
    ///                 new ContactMethod(
    ///                     new ContactMethod.Email(
    ///                         new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                     )
    ///                 ),
    ///                 new ContactMethod(
    ///                     new ContactMethod.Phone(new ContactMethodPhone { Value = "2025550164" })
    ///                 ),
    ///             },
    ///             Relationship = new OwnerRelationship
    ///             {
    ///                 EquityPercentage = 51.5f,
    ///                 Title = "CEO",
    ///                 IsControlProng = false,
    ///                 IsAuthorizedSignatory = true,
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Owner> CreateOwnerAsync(
        CreateOwnerFundingRecipientsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Owner>(
            CreateOwnerAsyncCore(request, options, cancellationToken)
        );
    }
}
