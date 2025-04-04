using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.Subscriptions;

public partial class SubscriptionsClient
{
    private RawClient _client;

    internal SubscriptionsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// List subscriptions linked to a terminal.
    /// To filter your results, use the query parameters.
    /// </summary>
    /// <example><code>
    /// await client.Payments.Subscriptions.ListAsync(
    ///     new ListSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         CustomerName = "Sarah%20Hazel%20Hopper",
    ///         Last4 = "7062",
    ///         PaymentPlan = "Premium%20Club",
    ///         EndDate = "2025-07-01T00:00:00Z",
    ///         NextDueDate = "2024-08-01T00:00:00Z",
    ///         Before = "2571",
    ///         After = "8516",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
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
                var _query = new Dictionary<string, object>();
                if (request.CustomerName != null)
                {
                    _query["customerName"] = request.CustomerName;
                }
                if (request.Last4 != null)
                {
                    _query["last4"] = request.Last4;
                }
                if (request.PaymentPlan != null)
                {
                    _query["paymentPlan"] = request.PaymentPlan;
                }
                if (request.Frequency != null)
                {
                    _query["frequency"] = request.Frequency.Value.Stringify();
                }
                if (request.Status != null)
                {
                    _query["status"] = request.Status.Value.Stringify();
                }
                if (request.EndDate != null)
                {
                    _query["endDate"] = request.EndDate;
                }
                if (request.NextDueDate != null)
                {
                    _query["nextDueDate"] = request.NextDueDate;
                }
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
                var _headers = new Headers(
                    new Dictionary<string, string>()
                    {
                        { "Idempotency-Key", request.IdempotencyKey },
                    }
                );
                var httpRequest = _client.CreateHttpRequest(
                    new RawClient.JsonApiRequest
                    {
                        BaseUrl = _client.Options.BaseUrl,
                        Method = HttpMethod.Get,
                        Path =
                            $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/subscriptions",
                        Query = _query,
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
                    .CreateAsync<Subscription>(sendRequest, httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Create a new subscription.
    /// </summary>
    /// <example><code>
    /// await client.Payments.Subscriptions.CreateAsync(
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
    ///         SetupOrder = new SubscriptionPaymentOrder
    ///         {
    ///             OrderId = "OrderRef6543",
    ///             Amount = 4999,
    ///             Description = "Initial setup fee for Premium Club subscription",
    ///             Breakdown = new SubscriptionOrderBreakdown
    ///             {
    ///                 Subtotal = 4347,
    ///                 Taxes = new List&lt;Tax&gt;()
    ///                 {
    ///                     new Tax { Name = "Sales Tax", Rate = 5 },
    ///                 },
    ///             },
    ///         },
    ///         RecurringOrder = new SubscriptionRecurringOrder
    ///         {
    ///             Amount = 4999,
    ///             Description = "Monthly Premium Club subscription",
    ///             Breakdown = new SubscriptionOrderBreakdown
    ///             {
    ///                 Subtotal = 4347,
    ///                 Taxes = new List&lt;Tax&gt;()
    ///                 {
    ///                     new Tax { Name = "Sales Tax", Rate = 5 },
    ///                 },
    ///             },
    ///         },
    ///         StartDate = "2024-07-02",
    ///         EndDate = "2025-07-01",
    ///         Length = 12,
    ///         PauseCollectionFor = 0,
    ///         CustomFields = new List&lt;CustomField&gt;()
    ///         {
    ///             new CustomField { Name = "yourCustomField", Value = "abc123" },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<Subscription> CreateAsync(
        SubscriptionRequest request,
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
                        new RawClient.JsonApiRequest
                        {
                            BaseUrl = _client.Options.BaseUrl,
                            Method = HttpMethod.Post,
                            Path =
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/subscriptions",
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
                        return JsonUtils.Deserialize<Subscription>(responseBody)!;
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
    /// Retrieve a specific subscription.
    /// </summary>
    /// <example><code>
    /// await client.Payments.Subscriptions.GetAsync(
    ///     new GetSubscriptionsRequest { ProcessingTerminalId = "1234001", SubscriptionId = "SubRef7654" }
    /// );
    /// </code></example>
    public async Task<Subscription> GetAsync(
        GetSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new RawClient.JsonApiRequest
                        {
                            BaseUrl = _client.Options.BaseUrl,
                            Method = HttpMethod.Get,
                            Path =
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/subscriptions/{JsonUtils.SerializeAsString(request.SubscriptionId)}",
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
                        return JsonUtils.Deserialize<Subscription>(responseBody)!;
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
    /// Make changes to a subscription.
    ///
    /// Structure your request to follow the RFC 6902 standard.
    /// </summary>
    /// <example><code>
    /// await client.Payments.Subscriptions.UpdateAsync(
    ///     new UpdateSubscriptionsRequest
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
    public async Task<Subscription> UpdateAsync(
        UpdateSubscriptionsRequest request,
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
                        new RawClient.JsonApiRequest
                        {
                            BaseUrl = _client.Options.BaseUrl,
                            Method = HttpMethodExtensions.Patch,
                            Path =
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/subscriptions/{JsonUtils.SerializeAsString(request.SubscriptionId)}",
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
                        return JsonUtils.Deserialize<Subscription>(responseBody)!;
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
    /// Deactivate a subscription.
    /// </summary>
    /// <example><code>
    /// await client.Payments.Subscriptions.DeactivateAsync(
    ///     new DeactivateSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SubscriptionId = "SubRef7654",
    ///     }
    /// );
    /// </code></example>
    public async Task<Subscription> DeactivateAsync(
        DeactivateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new RawClient.JsonApiRequest
                        {
                            BaseUrl = _client.Options.BaseUrl,
                            Method = HttpMethod.Post,
                            Path =
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/subscriptions/{JsonUtils.SerializeAsString(request.SubscriptionId)}/deactivate",
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
                        return JsonUtils.Deserialize<Subscription>(responseBody)!;
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
    /// Re-activate an existing subscription.
    /// </summary>
    /// <example><code>
    /// await client.Payments.Subscriptions.ReactivateAsync(
    ///     new ReactivateSubscriptionsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SubscriptionId = "SubRef7654",
    ///     }
    /// );
    /// </code></example>
    public async Task<Subscription> ReactivateAsync(
        ReactivateSubscriptionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new RawClient.JsonApiRequest
                        {
                            BaseUrl = _client.Options.BaseUrl,
                            Method = HttpMethod.Post,
                            Path =
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/subscriptions/{JsonUtils.SerializeAsString(request.SubscriptionId)}/reactivate",
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
                        return JsonUtils.Deserialize<Subscription>(responseBody)!;
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
    /// Process payment for a manual subscription.
    /// </summary>
    /// <example><code>
    /// await client.Payments.Subscriptions.PayAsync(
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
    ///             Breakdown = new SubscriptionOrderBreakdown
    ///             {
    ///                 Subtotal = 4999,
    ///                 Taxes = new List&lt;Tax&gt;()
    ///                 {
    ///                     new Tax { Name = "Sales Tax", Rate = 5 },
    ///                 },
    ///             },
    ///         },
    ///         CustomFields = new List&lt;CustomField&gt;()
    ///         {
    ///             new CustomField { Name = "yourCustomField", Value = "abc123" },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<SubscriptionPayment> PayAsync(
        SubscriptionPaymentRequest request,
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
                        new RawClient.JsonApiRequest
                        {
                            BaseUrl = _client.Options.BaseUrl,
                            Method = HttpMethod.Post,
                            Path =
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/subscriptions/{JsonUtils.SerializeAsString(request.SubscriptionId)}/pay",
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
                        return JsonUtils.Deserialize<SubscriptionPayment>(responseBody)!;
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
}
