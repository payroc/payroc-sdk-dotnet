using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.PaymentPlans;

public partial class PaymentPlansClient
{
    private RawClient _client;

    internal PaymentPlansClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to retrieve a [paginated](/api/pagination) list of payment plans for a processing terminal.
    ///
    /// **Note:** If you want to view a specific payment plan and you have its paymentPlanId, use our [Retrieve Payment Plan](/api/schema/payments/payment-plans/get) method.
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
    /// await client.Payments.PaymentPlans.ListAsync(
    ///     new ListPaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         Before = "2571",
    ///         After = "8516",
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
                            "processing-terminals/{0}/payment-plans",
                            ValueConvert.ToPathParameterString(request.ProcessingTerminalId)
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
    /// **Note:** This method is part of our Repeat Payments feature. To help you understand how this method works with our Subscriptions endpoints, go to [Repeat Payments](/guides/integrate/repeat-payments).
    ///
    /// When you create a payment plan you need to provide a unique paymentPlanId that you use to run follow-on actions:
    ///
    /// -	[Retrieve Payment Plan](/api/schema/payments/payment-plans/get)  - View the details of the payment plan.
    /// -	[Update Payment Plan](/api/schema/payments/payment-plans/update)  - Update the details of the payment plan.
    /// -	[Delete Payment Plan](/api/schema/payments/payment-plans/delete)  - Delete the payment plan.
    /// -	[Create Subscription](/api/schema/payments/subscriptions/create)  - Subscribe a customer to the payment plan.
    ///
    /// The request includes the following settings:
    ///
    /// -	**type** - Indicates if our gateway or the merchant collects payments. If the merchant manually collects payments, integrate with the [Pay Manual Subscription](/api/schema/payments/subscriptions/pay) method.
    /// -	**recurringOrder** - Amount of each payment if the gateway automatically collect payments.
    /// -	**setupOrder** - Setup fee that our gateway immediately collects from the customer's payment method.
    /// -	**onUpdate and onDelete** - Indicates what happens to associated subscriptions if the merchant updates or deletes the payment plan.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentPlans.CreateAsync(
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
    ///             SetupOrder = new PaymentPlanSetupOrder
    ///             {
    ///                 Amount = 4999,
    ///                 Description = "Initial setup fee for Premium Club subscription",
    ///                 Breakdown = new PaymentPlanOrderBreakdown
    ///                 {
    ///                     Subtotal = 4347,
    ///                     Taxes = new List&lt;Tax&gt;()
    ///                     {
    ///                         new Tax { Name = "Sales Tax", Rate = 5 },
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
    ///                     Taxes = new List&lt;Tax&gt;()
    ///                     {
    ///                         new Tax { Name = "Sales Tax", Rate = 5 },
    ///                     },
    ///                 },
    ///             },
    ///             Length = 12,
    ///             Type = PaymentPlanType.Automatic,
    ///             Frequency = PaymentPlanFrequency.Monthly,
    ///             OnUpdate = PaymentPlanOnUpdate.Continue,
    ///             OnDelete = PaymentPlanOnDelete.Complete,
    ///             CustomFieldNames = new List&lt;string&gt;() { "yourCustomField" },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<PaymentPlan> CreateAsync(
        CreatePaymentPlansRequest request,
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
                        return JsonUtils.Deserialize<PaymentPlan>(responseBody)!;
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
    /// Use this method to retrieve information about a payment plan.
    ///
    /// To retrieve a payment plan, you need its paymentPlanId. Our gateway returned the paymentPlanId in the response of the [Create Payment Plan](/api/schema/payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](/api/schema/payments/payment-plans/list) method to search for the payment plan.
    ///
    /// Our gateway returns the following information about the payment plan:
    ///
    ///   -	Name, length, and currency of the plan
    ///   -	How often our gateway collects each payment
    ///   -	How much our gateway collects for each payment
    ///   -	What happens if the merchant updates or deletes the plan
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentPlans.RetrieveAsync(
    ///     new RetrievePaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         PaymentPlanId = "PlanRef8765",
    ///     }
    /// );
    /// </code></example>
    public async Task<PaymentPlan> RetrieveAsync(
        RetrievePaymentPlansRequest request,
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
                                "processing-terminals/{0}/payment-plans/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.PaymentPlanId)
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
                        return JsonUtils.Deserialize<PaymentPlan>(responseBody)!;
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
    /// Use this method to delete a payment plan.
    ///
    /// &gt; **Important:** When you delete a payment plan, you can’t recover it. You also won’t be able to add subscriptions to the payment plan.
    ///
    /// To delete a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](/api/schema/payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](/api/schema/payments/payment-plans/list) method to search for the payment plan.
    ///
    /// The value you sent for the onDelete parameter when you created the payment plan indicates what happens to associated subscriptions when you delete the plan:
    ///
    ///   -	`complete` - Our gateway stops taking payments for the subscriptions associated with the payment plan.
    ///   -	`continue` - Our gateway continues to take payments for the subscriptions associated with the payment plan. To stop a subscription for a cancelled payment plan, go to the [Deactivate Subscription](/api/schema/payments/subscriptions/deactivate) method.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentPlans.DeleteAsync(
    ///     new DeletePaymentPlansRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         PaymentPlanId = "PlanRef8765",
    ///     }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task DeleteAsync(
        DeletePaymentPlansRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
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
    /// To update a payment plan, you need its paymentPlanId, which you sent in the request of the [Create Payment Plan](/api/schema/payments/payment-plans/create) method.
    ///
    /// **Note:** If you don't have the paymentPlanId, use our [List Payment Plans](/api/schema/payments/payment-plans/list) method to search for the payment plan.
    ///
    /// You can update all of the properties of the payment plan except for the paymentPlanId.
    ///
    /// The value you sent for the onUpdate parameter when you created the payment plan indicates what happens to the associated subscriptions when you update the plan:
    /// - `update` - Our gateway updates the subscriptions associated with the payment plan.
    /// - `continue` - Our  gateway doesn't update the subscriptions associated with the payment plan.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentPlans.PartiallyUpdateAsync(
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
    public async Task<PaymentPlan> PartiallyUpdateAsync(
        PartiallyUpdatePaymentPlansRequest request,
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
                        return JsonUtils.Deserialize<PaymentPlan>(responseBody)!;
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
