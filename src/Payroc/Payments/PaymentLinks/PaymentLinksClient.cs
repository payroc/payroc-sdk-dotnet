using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;
using Payroc.Payments.PaymentLinks.SharingEvents;

namespace Payroc.Payments.PaymentLinks;

public partial class PaymentLinksClient
{
    private RawClient _client;

    internal PaymentLinksClient(RawClient client)
    {
        _client = client;
        SharingEvents = new SharingEventsClient(_client);
    }

    public SharingEventsClient SharingEvents { get; }

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payment links linked to a processing terminal.
    ///
    /// **Note:** If you want to view the details of a specific payment link and you have its paymentLinkId, use our [Retrieve Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for only active links or multi-use links.
    ///
    /// Our gateway returns the following information about each payment link in the list:
    /// - **type** - Indicates whether the link can be used only once or if it can be used multiple times.
    /// - **authType** - Indicates whether the transaction is a sale or a pre-authorization.
    /// - **paymentMethods** - Indicates the payment method that the merchant accepts.
    /// - **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
    /// - **status** - Indicates if the payment link is active.
    ///
    /// For each payment link, we also return a paymentLinkId, which you can use for follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentLinks.ListAsync(
    ///     new ListPaymentLinksRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         MerchantReference = "LinkRef6543",
    ///         RecipientName = "Sarah Hazel Hopper",
    ///         RecipientEmail = "sarah.hopper@example.com",
    ///         CreatedOn = new DateOnly(2024, 7, 2),
    ///         ExpiresOn = new DateOnly(2024, 8, 2),
    ///         Before = "2571",
    ///         After = "8516",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<PaymentLinkPaginatedListDataItem>> ListAsync(
        ListPaymentLinksRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                if (request.MerchantReference != null)
                {
                    _query["merchantReference"] = request.MerchantReference;
                }
                if (request.LinkType != null)
                {
                    _query["linkType"] = request.LinkType.Value.Stringify();
                }
                if (request.ChargeType != null)
                {
                    _query["chargeType"] = request.ChargeType.Value.Stringify();
                }
                if (request.Status != null)
                {
                    _query["status"] = request.Status.Value.Stringify();
                }
                if (request.RecipientName != null)
                {
                    _query["recipientName"] = request.RecipientName;
                }
                if (request.RecipientEmail != null)
                {
                    _query["recipientEmail"] = request.RecipientEmail;
                }
                if (request.CreatedOn != null)
                {
                    _query["createdOn"] = request.CreatedOn.Value.ToString(Constants.DateFormat);
                }
                if (request.ExpiresOn != null)
                {
                    _query["expiresOn"] = request.ExpiresOn.Value.ToString(Constants.DateFormat);
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
                var httpRequest = _client.CreateHttpRequest(
                    new JsonRequest
                    {
                        BaseUrl = _client.Options.Environment.Api,
                        Method = HttpMethod.Get,
                        Path = string.Format(
                            "processing-terminals/{0}/payment-links",
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
                };
                return await PayrocPagerFactory
                    .CreateAsync<PaymentLinkPaginatedListDataItem>(
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
    /// Use this method to create a payment link that a customer can use to make a payment for goods or services.
    ///
    /// The request includes the following settings:
    /// - **type** - Indicates whether the link can be used only once or if it can be used multiple times.
    /// - **authType** - Indicates whether the transaction is a sale or a pre-authorization.
    /// - **paymentMethod** - Indicates the payment methods that the merchant accepts.
    /// - **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
    ///
    /// If your request is successful, our gateway returns a paymentLinkId, which you can use to perform follow-on actions.
    ///
    /// **Note:** To share the payment link with a customer, use our [Share Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/sharing-events/share) method.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentLinks.CreateAsync(
    ///     new CreatePaymentLinksRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new CreatePaymentLinksRequestBody(
    ///             new CreatePaymentLinksRequestBody.MultiUse(
    ///                 new MultiUsePaymentLink
    ///                 {
    ///                     MerchantReference = "LinkRef6543",
    ///                     Order = new MultiUsePaymentLinkOrder
    ///                     {
    ///                         Charge = new MultiUsePaymentLinkOrderCharge(
    ///                             new MultiUsePaymentLinkOrderCharge.Prompt(
    ///                                 new PromptPaymentLinkCharge { Currency = Currency.Aed }
    ///                             )
    ///                         ),
    ///                     },
    ///                     AuthType = MultiUsePaymentLinkAuthType.Sale,
    ///                     PaymentMethods = new List&lt;MultiUsePaymentLinkPaymentMethodsItem&gt;()
    ///                     {
    ///                         MultiUsePaymentLinkPaymentMethodsItem.Card,
    ///                     },
    ///                 }
    ///             )
    ///         ),
    ///     }
    /// );
    /// </code></example>
    public async Task<CreatePaymentLinksResponse> CreateAsync(
        CreatePaymentLinksRequest request,
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
                                "processing-terminals/{0}/payment-links",
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
                        return JsonUtils.Deserialize<CreatePaymentLinksResponse>(responseBody)!;
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
    /// Use this method to retrieve information about a payment link.
    ///
    /// To retrieve a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.
    ///
    /// Our gateway returns the following information about the payment link:
    /// - **type** - Indicates whether the link can be used only once or if it can be used multiple times.
    /// - **authType** - Indicates whether the transaction is a sale or a pre-authorization.
    /// - **paymentMethods** - Indicates the payment method that the merchant accepts.
    /// - **charge** - Indicates whether the merchant or the customer enters the amount for the transaction.
    /// - **status** - Indicates if the payment link is active.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentLinks.RetrieveAsync(
    ///     new RetrievePaymentLinksRequest { PaymentLinkId = "JZURRJBUPS" }
    /// );
    /// </code></example>
    public async Task<RetrievePaymentLinksResponse> RetrieveAsync(
        RetrievePaymentLinksRequest request,
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
                                "payment-links/{0}",
                                ValueConvert.ToPathParameterString(request.PaymentLinkId)
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
                        return JsonUtils.Deserialize<RetrievePaymentLinksResponse>(responseBody)!;
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
    /// Use this method to partially update a payment link. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a payment link, you need its paymentLinkId, which we sent you in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.
    ///
    /// You can update the following properties of a multi-use link:
    /// - **expiresOn parameter** - Expiration date of the link.
    /// - **customLabels object** - Label for the payment button.
    /// - **credentialOnFile object** - Settings for saving the customer's payment details.
    ///
    /// You can update the following properties of a single-use link:
    /// - **expiresOn parameter** - Expiration date of the link.
    /// - **authType parameter** - Transaction type of the payment link.
    /// - **amount parameter** - Total amount of the transaction.
    /// - **currency parameter** - Currency of the transaction.
    /// - **description parameter** - Brief description of the transaction.
    /// - **customLabels object** - Label for the payment button.
    /// - **credentialOnFile object** - Settings for saving the customer's payment details.
    ///
    /// **Note:** When a merchant updates a single-use link, we update the payment URL and HTML code in the assets object. The customer can't use the original link to make a payment.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentLinks.PartiallyUpdateAsync(
    ///     new PartiallyUpdatePaymentLinksRequest
    ///     {
    ///         PaymentLinkId = "JZURRJBUPS",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new List&lt;PatchDocument&gt;()
    ///         {
    ///             new PatchDocument(new PatchDocument.Remove(new PatchRemove { Path = "path" })),
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<PartiallyUpdatePaymentLinksResponse> PartiallyUpdateAsync(
        PartiallyUpdatePaymentLinksRequest request,
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
                                "payment-links/{0}",
                                ValueConvert.ToPathParameterString(request.PaymentLinkId)
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
                        return JsonUtils.Deserialize<PartiallyUpdatePaymentLinksResponse>(
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
    /// Use this method to deactivate a payment link.
    ///
    /// To deactivate a payment link, you need its paymentLinkId. Our gateway returned the paymentLinkId in the response of the [Create Payment Link](https://docs.payroc.com/api/schema/payments/payment-links/create) method.
    ///
    /// **Note:** If you don't have the paymentLinkId, use our [List Payment Links](https://docs.payroc.com/api/schema/payments/payment-links/list) method to search for the payment link.
    ///
    /// If your request is successful, our gateway deactivates the payment link. The customer can't use the link to make a payment, and you can't reactivate the payment link.
    /// </summary>
    /// <example><code>
    /// await client.Payments.PaymentLinks.DeactivateAsync(
    ///     new DeactivatePaymentLinksRequest { PaymentLinkId = "JZURRJBUPS" }
    /// );
    /// </code></example>
    public async Task<DeactivatePaymentLinksResponse> DeactivateAsync(
        DeactivatePaymentLinksRequest request,
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
                                "payment-links/{0}/deactivate",
                                ValueConvert.ToPathParameterString(request.PaymentLinkId)
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
                        return JsonUtils.Deserialize<DeactivatePaymentLinksResponse>(responseBody)!;
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
}
