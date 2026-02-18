using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Payments;

public partial class PaymentsClient : IPaymentsClient
{
    private RawClient _client;

    internal PaymentsClient(RawClient client)
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

    private async Task<WithRawResponse<BankTransferPayment>> CreateAsyncCore(
        BankTransferPaymentRequest request,
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
                            Path = "bank-transfer-payments",
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
                        var responseData = JsonUtils.Deserialize<BankTransferPayment>(
                            responseBody
                        )!;
                        return new WithRawResponse<BankTransferPayment>()
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

    private async Task<WithRawResponse<BankTransferPayment>> RetrieveAsyncCore(
        RetrievePaymentsRequest request,
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
                                "bank-transfer-payments/{0}",
                                ValueConvert.ToPathParameterString(request.PaymentId)
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
                        var responseData = JsonUtils.Deserialize<BankTransferPayment>(
                            responseBody
                        )!;
                        return new WithRawResponse<BankTransferPayment>()
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

    private async Task<WithRawResponse<BankTransferPayment>> RepresentAsyncCore(
        Representment request,
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
                                "bank-transfer-payments/{0}/represent",
                                ValueConvert.ToPathParameterString(request.PaymentId)
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
                        var responseData = JsonUtils.Deserialize<BankTransferPayment>(
                            responseBody
                        )!;
                        return new WithRawResponse<BankTransferPayment>()
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
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments.
    ///
    /// **Note:** If you want to view the details of a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for payments for a customer, a date range, or a settlement state.
    ///
    /// Our gateway returns the following information about each payment in the list:
    ///
    /// - Order details, including the transaction amount and when it was processed.
    /// - Bank account details, including the customer’s name and account number.
    /// - Customer's details, including the customer’s phone number.
    /// - Transaction details, including any refunds or re-presentments.
    ///
    /// For each transaction, we also return the paymentId and an optional secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.BankTransferPayments.Payments.ListAsync(
    ///     new Payroc.BankTransferPayments.Payments.ListPaymentsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         OrderId = "OrderRef6543",
    ///         NameOnAccount = "Sarah%20Hazel%20Hopper",
    ///         Last4 = "7890",
    ///         DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
    ///         DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
    ///         SettlementState = Payroc
    ///             .BankTransferPayments
    ///             .Payments
    ///             .ListPaymentsRequestSettlementState
    ///             .Settled,
    ///         SettlementDate = new DateOnly(2024, 7, 15),
    ///         PaymentLinkId = "JZURRJBUPS",
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<BankTransferPayment>> ListAsync(
        ListPaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 14)
                    .Add("processingTerminalId", request.ProcessingTerminalId)
                    .Add("orderId", request.OrderId)
                    .Add("nameOnAccount", request.NameOnAccount)
                    .Add("last4", request.Last4)
                    .Add("type", request.Type)
                    .Add("status", request.Status)
                    .Add("dateFrom", request.DateFrom)
                    .Add("dateTo", request.DateTo)
                    .Add("settlementState", request.SettlementState)
                    .Add("settlementDate", request.SettlementDate)
                    .Add("paymentLinkId", request.PaymentLinkId)
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
                        Path = "bank-transfer-payments",
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
                    .CreateAsync<BankTransferPayment>(
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
    /// Use this method to run a sale with a customer's bank account details.
    ///
    /// In the response, our gateway returns information about the bank transfer payment and a paymentId, which you need for the following methods:
    /// -	[Retrieve payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) - View the details of the bank transfer payment.
    /// -	[Reverse payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/reverse-payment) - Cancel the bank transfer payment if it's an open batch.
    /// -	[Refund payment](https://docs.payroc.com/api/schema/bank-transfer-payments/refunds/refund) - Run a referenced refund to return funds to the customer's bank account.
    ///
    /// **Payment methods**
    ///
    /// Our gateway accepts the following payment methods:
    /// -	Automated clearing house (ACH) details
    /// -	Pre-authorized debit (PAD) details
    ///
    /// You can also use [secure tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/overview) and [single-use tokens](https://docs.payroc.com/api/schema/tokenization/single-use-tokens/create) that you created from ACH details or PAD details.
    /// </summary>
    /// <example><code>
    /// await client.BankTransferPayments.Payments.CreateAsync(
    ///     new BankTransferPaymentRequest
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         ProcessingTerminalId = "1234001",
    ///         Order = new BankTransferPaymentRequestOrder
    ///         {
    ///             OrderId = "OrderRef6543",
    ///             Description = "Large Pepperoni Pizza",
    ///             Amount = 4999,
    ///             Currency = Currency.Usd,
    ///             Breakdown = new BankTransferRequestBreakdown
    ///             {
    ///                 Subtotal = 4347,
    ///                 Tip = new Tip { Type = TipType.Percentage, Percentage = 10 },
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
    ///         Customer = new BankTransferCustomer
    ///         {
    ///             NotificationLanguage = BankTransferCustomerNotificationLanguage.En,
    ///             ContactMethods = new List&lt;ContactMethod&gt;()
    ///             {
    ///                 new ContactMethod(
    ///                     new ContactMethod.Email(
    ///                         new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                     )
    ///                 ),
    ///             },
    ///         },
    ///         CredentialOnFile = new SchemasCredentialOnFile { Tokenize = true },
    ///         PaymentMethod = new BankTransferPaymentRequestPaymentMethod(
    ///             new Payroc.BankTransferPayments.Payments.BankTransferPaymentRequestPaymentMethod.Ach(
    ///                 new AchPayload
    ///                 {
    ///                     NameOnAccount = "Shara Hazel Hopper",
    ///                     AccountNumber = "1234567890",
    ///                     RoutingNumber = "123456789",
    ///                 }
    ///             )
    ///         ),
    ///         CustomFields = new List&lt;CustomField&gt;()
    ///         {
    ///             new CustomField { Name = "yourCustomField", Value = "abc123" },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<BankTransferPayment> CreateAsync(
        BankTransferPaymentRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<BankTransferPayment>(
            CreateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to retrieve information about a bank transfer payment.
    ///
    /// To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/create) method.
    ///
    /// Note: If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/list) method to search for the payment.
    ///
    /// Our gateway returns the following information about the payment:
    ///
    /// -	Order details, including the transaction amount and when it was processed.
    /// -	Bank account details, including the customer’s name and account number.
    /// -	Customer’s details, including the customer’s phone number.
    /// -	Transaction details, including any refunds or re-presentments.
    ///
    /// If the merchant saved the customer’s bank account details, our gateway returns a secureTokenID, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.BankTransferPayments.Payments.RetrieveAsync(
    ///     new Payroc.BankTransferPayments.Payments.RetrievePaymentsRequest { PaymentId = "M2MJOG6O2Y" }
    /// );
    /// </code></example>
    public WithRawResponseTask<BankTransferPayment> RetrieveAsync(
        RetrievePaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<BankTransferPayment>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to re-present an ACH payment.
    ///
    /// To re-present a payment, you need the paymentId of the return. To get the paymentId of the return, complete the following steps:
    ///
    /// 1.	Use our [Retrieve Payment](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve) method  to view the details of the original payment.
    /// 2.	From the [returns object](https://docs.payroc.com/api/schema/bank-transfer-payments/payments/retrieve#response.body.returns) in the response, get the paymentId of the return.
    ///
    /// Our gateway uses the bank account details from the original payment. If you want to update the customer's bank account details, send the new bank account details in the request.
    ///
    /// If your request is successful, our gateway re-presents the payment.
    /// </summary>
    /// <example><code>
    /// await client.BankTransferPayments.Payments.RepresentAsync(
    ///     new Representment
    ///     {
    ///         PaymentId = "M2MJOG6O2Y",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         PaymentMethod = new RepresentmentPaymentMethod(
    ///             new RepresentmentPaymentMethod.Ach(
    ///                 new AchPayload
    ///                 {
    ///                     NameOnAccount = "Shara Hazel Hopper",
    ///                     AccountNumber = "1234567890",
    ///                     RoutingNumber = "123456789",
    ///                 }
    ///             )
    ///         ),
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<BankTransferPayment> RepresentAsync(
        Representment request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<BankTransferPayment>(
            RepresentAsyncCore(request, options, cancellationToken)
        );
    }
}
