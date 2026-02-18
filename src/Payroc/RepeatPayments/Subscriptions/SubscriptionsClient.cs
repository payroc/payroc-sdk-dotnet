using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.RepeatPayments.Subscriptions;

public partial class SubscriptionsClient : ISubscriptionsClient
{
    private RawClient _client;

    internal SubscriptionsClient(RawClient client)
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

    private async Task<WithRawResponse<Subscription>> CreateAsyncCore(
        SubscriptionRequest request,
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
                                "processing-terminals/{0}/subscriptions",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId)
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
                        var responseData = JsonUtils.Deserialize<Subscription>(responseBody)!;
                        return new WithRawResponse<Subscription>()
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
                            case 415:
                                throw new UnsupportedMediaTypeError(
                                    JsonUtils.Deserialize<FourHundredFifteen>(responseBody)
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

    private async Task<WithRawResponse<Subscription>> RetrieveAsyncCore(
        RetrieveSubscriptionsRequest request,
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
                                "processing-terminals/{0}/subscriptions/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
                        var responseData = JsonUtils.Deserialize<Subscription>(responseBody)!;
                        return new WithRawResponse<Subscription>()
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

    private async Task<WithRawResponse<Subscription>> PartiallyUpdateAsyncCore(
        PartiallyUpdateSubscriptionsRequest request,
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
                            Method = HttpMethodExtensions.Patch,
                            Path = string.Format(
                                "processing-terminals/{0}/subscriptions/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
                        var responseData = JsonUtils.Deserialize<Subscription>(responseBody)!;
                        return new WithRawResponse<Subscription>()
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
                            case 415:
                                throw new UnsupportedMediaTypeError(
                                    JsonUtils.Deserialize<FourHundredFifteen>(responseBody)
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

    private async Task<WithRawResponse<Subscription>> DeactivateAsyncCore(
        DeactivateSubscriptionsRequest request,
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
                            Method = HttpMethod.Post,
                            Path = string.Format(
                                "processing-terminals/{0}/subscriptions/{1}/deactivate",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
                        var responseData = JsonUtils.Deserialize<Subscription>(responseBody)!;
                        return new WithRawResponse<Subscription>()
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
                            case 415:
                                throw new UnsupportedMediaTypeError(
                                    JsonUtils.Deserialize<FourHundredFifteen>(responseBody)
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

    private async Task<WithRawResponse<Subscription>> ReactivateAsyncCore(
        ReactivateSubscriptionsRequest request,
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
                            Method = HttpMethod.Post,
                            Path = string.Format(
                                "processing-terminals/{0}/subscriptions/{1}/reactivate",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
                        var responseData = JsonUtils.Deserialize<Subscription>(responseBody)!;
                        return new WithRawResponse<Subscription>()
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
                            case 415:
                                throw new UnsupportedMediaTypeError(
                                    JsonUtils.Deserialize<FourHundredFifteen>(responseBody)
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

    private async Task<WithRawResponse<SubscriptionPayment>> PayAsyncCore(
        SubscriptionPaymentRequest request,
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
                                "processing-terminals/{0}/subscriptions/{1}/pay",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SubscriptionId)
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
                        var responseData = JsonUtils.Deserialize<SubscriptionPayment>(
                            responseBody
                        )!;
                        return new WithRawResponse<SubscriptionPayment>()
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
                            case 415:
                                throw new UnsupportedMediaTypeError(
                                    JsonUtils.Deserialize<FourHundredFifteen>(responseBody)
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
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of subscriptions.
    ///
    /// Note: If you want to view the details of a specific subscription and you have its subscriptionId, use our [Retrieve subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for subscriptions for a customer, a payment plan, or frequency.
    ///
    /// Our gateway returns information about the following for each subscription in the list:
    ///
    /// -	Payment plan the subscription is linked to.
    /// -	Secure token that represents cardholder’s payment details.
    /// -	Current state of the subscription, including its status, next due date, and invoices.
    /// -	Fees for setup and the cost of the recurring order.
    /// -	Subscription length, end date, and frequency.
    ///
    /// For each subscription, we also return the subscriptionId, the paymentPlanId, and the secureTokenId, which you can use to perform follow-actions.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.Subscriptions.ListAsync(
    ///     new ListSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         CustomerName = "Sarah%20Hazel%20Hopper",
    ///         Last4 = "7062",
    ///         PaymentPlan = "Premium%20Club",
    ///         Frequency = ListSubscriptionsRequestFrequency.Weekly,
    ///         Status = ListSubscriptionsRequestStatus.Active,
    ///         EndDate = new DateOnly(2025, 7, 1),
    ///         NextDueDate = new DateOnly(2024, 8, 1),
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<Subscription>> ListAsync(
        ListSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 10)
                    .Add("customerName", request.CustomerName)
                    .Add("last4", request.Last4)
                    .Add("paymentPlan", request.PaymentPlan)
                    .Add("frequency", request.Frequency)
                    .Add("status", request.Status)
                    .Add("endDate", request.EndDate)
                    .Add("nextDueDate", request.NextDueDate)
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
                            "processing-terminals/{0}/subscriptions",
                            ValueConvert.ToPathParameterString(request.ProcessingTerminalId)
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
                    .CreateAsync<Subscription>(
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
    /// Use this method to assign a customer to a payment plan.
    ///
    /// **Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Payment plans endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/take-payments/repeat-payments).
    ///
    /// When you create a subscription you need to provide a unique subscriptionId that you use to run follow-on actions:
    ///
    /// - [Retrieve Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/retrieve) - View the details of the subscription.
    /// - [Update Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/partially-update) - Update the details of the subscription.
    /// - [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) - Stop taking payments for the subscription.
    /// - [Re-activate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/reactivate) - Start taking payments again for the subscription.
    /// - [Pay Manual Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/pay) - Manually collect a payment for the subscription.
    ///
    /// The request includes the following settings:
    /// - **paymentPlanId** - Unique identifier of the payment plan that the merchant wants to use. If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    /// - **paymentMethod** - Object that contains information about the secure token, which represents the customer's card details or bank account details.
    /// - **startDate** - Date that you want to start to take payments.
    ///
    /// You can also update the settings that the subscription inherited from the payment plan, for example, you can change the amount for each payment. If you change the settings for the subscription, it doesn't change the settings in the payment plan that it's linked to.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.Subscriptions.CreateAsync(
    ///     new SubscriptionRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         SubscriptionId = "SubRef7654",
    ///         PaymentPlanId = "PlanRef8765",
    ///         PaymentMethod = new SubscriptionRequestPaymentMethod(
    ///             new SubscriptionRequestPaymentMethod.SecureToken(
    ///                 new SecureTokenPayload { Token = "1234567890123456789" }
    ///             )
    ///         ),
    ///         Name = "Premium Club",
    ///         Description = "Premium Club subscription",
    ///         SetupOrder = new SubscriptionPaymentOrderRequest
    ///         {
    ///             OrderId = "OrderRef6543",
    ///             Amount = 4999,
    ///             Description = "Initial setup fee for Premium Club subscription",
    ///         },
    ///         RecurringOrder = new SubscriptionRecurringOrderRequest
    ///         {
    ///             Amount = 4999,
    ///             Description = "Monthly Premium Club subscription",
    ///             Breakdown = new SubscriptionOrderBreakdownRequest
    ///             {
    ///                 Subtotal = 4347,
    ///                 Taxes = new List&lt;TaxRate&gt;()
    ///                 {
    ///                     new TaxRate
    ///                     {
    ///                         Type = TaxRateType.Rate,
    ///                         Rate = 5,
    ///                         Name = "Sales Tax",
    ///                     },
    ///                 },
    ///             },
    ///         },
    ///         StartDate = new DateOnly(2024, 7, 2),
    ///         EndDate = new DateOnly(2025, 7, 1),
    ///         Length = 12,
    ///         PauseCollectionFor = 0,
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Subscription> CreateAsync(
        SubscriptionRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Subscription>(
            CreateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to retrieve information about a subscription.
    ///
    /// To retrieve a subscription, you need its subscriptionId. You sent the subscriptionId in the request of the [Create subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// Our gateway returns information about the following for the subscription:
    ///
    /// -	Payment plan the subscription is linked to.
    /// -	Secure token that represents cardholder’s payment details.
    /// -	Current state of the subscription, including its status, next due date, and invoices.
    /// -	Fees for setup and the cost of the recurring order.
    /// -	Subscription length, end date, and frequency.
    ///
    /// We also return the paymentPlanId and the secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.Subscriptions.RetrieveAsync(
    ///     new RetrieveSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SubscriptionId = "SubRef7654",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Subscription> RetrieveAsync(
        RetrieveSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Subscription>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to partially update a subscription. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a subscription, you need its subscriptionId, which you sent in the request of the [Create subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the payment.
    ///
    /// You can update all of the properties of the subscription except for the following:
    ///
    /// **Can't delete**
    /// - recurringOrder
    /// - description
    /// - name
    ///
    /// **Can't perform any PATCH operation**
    /// - currentState
    /// - type
    /// - frequency
    /// - paymentPlan
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.Subscriptions.PartiallyUpdateAsync(
    ///     new PartiallyUpdateSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SubscriptionId = "SubRef7654",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new List&lt;PatchDocument&gt;()
    ///         {
    ///             new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
    ///             new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
    ///             new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Subscription> PartiallyUpdateAsync(
        PartiallyUpdateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Subscription>(
            PartiallyUpdateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to deactivate a subscription.
    ///
    /// To deactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// If your request is successful, our gateway stops taking payments from the customer.
    ///
    /// To reactivate the subscription, use our [Reactivate Subscription](https://docs.payroc.com/api/schema/payments/subscriptions/reactivate) method.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.Subscriptions.DeactivateAsync(
    ///     new DeactivateSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SubscriptionId = "SubRef7654",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Subscription> DeactivateAsync(
        DeactivateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Subscription>(
            DeactivateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to reactivate a subscription.
    ///
    /// To reactivate a subscription, you need its subscriptionId, which you sent in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// If your request is successful, our gateway restarts taking payments from the customer.
    ///
    /// To deactivate the subscription, use our [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) method.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.Subscriptions.ReactivateAsync(
    ///     new ReactivateSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SubscriptionId = "SubRef7654",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Subscription> ReactivateAsync(
        ReactivateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Subscription>(
            ReactivateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to manually collect a payment linked to a subscription. You can manually collect a payment only if the merchant chose not to let our gateway automatically collect each payment.
    ///
    /// To manually collect a payment, you need the subscriptionId of the subscription that's linked to the payment. You sent the subscriptionId in the request of the [Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create) method.
    ///
    /// **Note:** If you don't have the subscriptionId, use our [List Subscriptions](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/list) method to search for the subscription.
    ///
    /// The request includes an order object that contains information about the amount that you want to collect.
    ///
    /// In the response, our gateway returns information about the payment and a paymentId. You can use the paymentId in follow-on actions with the [Payments](https://docs.payroc.com/api/schema/card-payments/payments) endpoints or [Bank Transfer Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments) endpoints.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.Subscriptions.PayAsync(
    ///     new SubscriptionPaymentRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SubscriptionId = "SubRef7654",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Operator = "Jane",
    ///         Order = new SubscriptionPaymentOrder
    ///         {
    ///             OrderId = "OrderRef6543",
    ///             Amount = 4999,
    ///             Description = "Monthly Premium Club subscription",
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<SubscriptionPayment> PayAsync(
        SubscriptionPaymentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<SubscriptionPayment>(
            PayAsyncCore(request, options, cancellationToken)
        );
    }
}
