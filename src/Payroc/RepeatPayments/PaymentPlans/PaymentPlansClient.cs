using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.RepeatPayments.PaymentPlans;

public partial class PaymentPlansClient : IPaymentPlansClient
{
    private RawClient _client;

    internal PaymentPlansClient(RawClient client)
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

    private async Task<WithRawResponse<PaymentPlan>> CreateAsyncCore(
        CreatePaymentPlansRequest request,
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
                                "processing-terminals/{0}/payment-plans",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId)
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
                        var responseData = JsonUtils.Deserialize<PaymentPlan>(responseBody)!;
                        return new WithRawResponse<PaymentPlan>()
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

    private async Task<WithRawResponse<PaymentPlan>> RetrieveAsyncCore(
        RetrievePaymentPlansRequest request,
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
                                "processing-terminals/{0}/payment-plans/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.PaymentPlanId)
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
                        var responseData = JsonUtils.Deserialize<PaymentPlan>(responseBody)!;
                        return new WithRawResponse<PaymentPlan>()
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

    private async Task<WithRawResponse<PaymentPlan>> PartiallyUpdateAsyncCore(
        PartiallyUpdatePaymentPlansRequest request,
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
                                "processing-terminals/{0}/payment-plans/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.PaymentPlanId)
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
                        var responseData = JsonUtils.Deserialize<PaymentPlan>(responseBody)!;
                        return new WithRawResponse<PaymentPlan>()
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
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payment plans for a processing terminal.
    ///
    /// **Note:** If you want to view the details of a specific payment plan and you have its paymentPlanId, use our [Retrieve Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/retrieve) method.
    ///
    /// Our gateway returns the following information about each payment plan in the list:
    ///
    ///   -	Name, length, and currency of the plan
    ///   -	How often our gateway collects each payment
    ///   -	How much our gateway collects for each payment
    ///   -	What happens if the merchant updates or deletes the plan
    ///
    /// For each payment plan, we return the paymentPlanId, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.PaymentPlans.ListAsync(
    ///     new ListPaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<PaymentPlan>> ListAsync(
        ListPaymentPlansRequest request,
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
                            "processing-terminals/{0}/payment-plans",
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
                    .CreateAsync<PaymentPlan>(
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
    /// Use this method to create a payment schedule that you can assign customers to.
    ///
    /// **Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Subscriptions endpoints, go to [Repeat Payments](https://docs.payroc.com/guides/take-payments/repeat-payments).
    ///
    /// When you create a payment plan you need to provide a unique paymentPlanId that you use to run follow-on actions:
    ///
    /// -	[Retrieve Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/retrieve)  - View the details of the payment plan.
    /// -	[Update Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/partially-update)  - Update the details of the payment plan.
    /// -	[Delete Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/delete)  - Delete the payment plan.
    /// -	[Create Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/create)  - Subscribe a customer to the payment plan.
    ///
    /// The request includes the following settings:
    ///
    /// -	**type** - Indicates if our gateway or the merchant collects payments. If the merchant manually collects payments, integrate with the [Pay Manual Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/pay) method.
    /// -	**recurringOrder** - Amount of each payment if the gateway automatically collect payments.
    /// -	**setupOrder** - Setup fee that our gateway immediately collects from the customer's payment method.
    /// -	**onUpdate and onDelete** - Indicates what happens to associated subscriptions if the merchant updates or deletes the payment plan.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.PaymentPlans.CreateAsync(
    ///     new CreatePaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new PaymentPlan
    ///         {
    ///             PaymentPlanId = "PlanRef8765",
    ///             Name = "Premium Club",
    ///             Description = "Monthly Premium Club subscription",
    ///             Currency = Currency.Usd,
    ///             Length = 12,
    ///             Type = PaymentPlanBaseType.Automatic,
    ///             Frequency = PaymentPlanBaseFrequency.Monthly,
    ///             OnUpdate = PaymentPlanBaseOnUpdate.Continue,
    ///             OnDelete = PaymentPlanBaseOnDelete.Complete,
    ///             CustomFieldNames = new List&lt;string&gt;() { "yourCustomField" },
    ///             SetupOrder = new PaymentPlanSetupOrder
    ///             {
    ///                 Amount = 4999,
    ///                 Description = "Initial setup fee for Premium Club subscription",
    ///                 Breakdown = new PaymentPlanOrderBreakdown
    ///                 {
    ///                     Subtotal = 4347,
    ///                     Taxes = new List&lt;RetrievedTax&gt;()
    ///                     {
    ///                         new RetrievedTax { Name = "Sales Tax", Rate = 5 },
    ///                     },
    ///                 },
    ///             },
    ///             RecurringOrder = new PaymentPlanRecurringOrder
    ///             {
    ///                 Amount = 4999,
    ///                 Description = "Monthly Premium Club subscription",
    ///                 Breakdown = new PaymentPlanOrderBreakdown
    ///                 {
    ///                     Subtotal = 4347,
    ///                     Taxes = new List&lt;RetrievedTax&gt;()
    ///                     {
    ///                         new RetrievedTax { Name = "Sales Tax", Rate = 5 },
    ///                     },
    ///                 },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<PaymentPlan> CreateAsync(
        CreatePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<PaymentPlan>(
            CreateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to retrieve information about a payment plan.
    ///
    /// To retrieve a payment plan, you need its paymentPlanId. Our gateway returned the paymentPlanId in the response of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    ///
    /// Our gateway returns the following information about the payment plan:
    ///
    ///   -	Name, length, and currency of the plan
    ///   -	How often our gateway collects each payment
    ///   -	How much our gateway collects for each payment
    ///   -	What happens if the merchant updates or deletes the plan
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.PaymentPlans.RetrieveAsync(
    ///     new RetrievePaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         PaymentPlanId = "PlanRef8765",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<PaymentPlan> RetrieveAsync(
        RetrievePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<PaymentPlan>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to delete a payment plan.
    ///
    /// &gt; **Important:** When you delete a payment plan, you can’t recover it. You also won’t be able to add subscriptions to the payment plan.
    ///
    /// To delete a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    ///
    /// The value you sent for the onDelete parameter when you created the payment plan indicates what happens to associated subscriptions when you delete the plan:
    ///
    ///   -	`complete` - Our gateway stops taking payments for the subscriptions associated with the payment plan.
    ///   -	`continue` - Our gateway continues to take payments for the subscriptions associated with the payment plan. To stop a subscription for a cancelled payment plan, go to the [Deactivate Subscription](https://docs.payroc.com/api/schema/repeat-payments/subscriptions/deactivate) method.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.PaymentPlans.DeleteAsync(
    ///     new DeletePaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         PaymentPlanId = "PlanRef8765",
    ///     }
    /// );
    /// </code></example>
    public async Task DeleteAsync(
        DeletePaymentPlansRequest request,
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
                                "processing-terminals/{0}/payment-plans/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.PaymentPlanId)
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
    /// Use this method to partially update a payment plan. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](https://docs.payroc.com/api/schema/repeat-payments/payment-plans/list) method to search for the payment plan.
    ///
    /// You can update all of the properties of the payment plan except for the paymentPlanId.
    ///
    /// The value you sent for the onUpdate parameter when you created the payment plan indicates what happens to the associated subscriptions when you update the plan:
    /// - `update` - Our gateway updates the subscriptions associated with the payment plan.
    /// - `continue` - Our  gateway doesn't update the subscriptions associated with the payment plan.
    /// </summary>
    /// <example><code>
    /// await client.RepeatPayments.PaymentPlans.PartiallyUpdateAsync(
    ///     new PartiallyUpdatePaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         PaymentPlanId = "PlanRef8765",
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
    public WithRawResponseTask<PaymentPlan> PartiallyUpdateAsync(
        PartiallyUpdatePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<PaymentPlan>(
            PartiallyUpdateAsyncCore(request, options, cancellationToken)
        );
    }
}
