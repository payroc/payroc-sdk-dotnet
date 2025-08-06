using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

public partial class BankTransferPaymentsClient
{
    private RawClient _client;

    internal BankTransferPaymentsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments.
    ///
    /// **Note:** If you want to view a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve) method.
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
    /// await client.Payments.BankTransferPayments.ListAsync(
    ///     new ListBankTransferPaymentsRequest { ProcessingTerminalId = "processingTerminalId" }
    /// );
    /// </code></example>
    public async Task<PayrocPager<BankTransferPayment>> ListAsync(
        ListBankTransferPaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["processingTerminalId"] = request.ProcessingTerminalId;
                _query["type"] = request.Type.Select(_value => _value.Stringify()).ToList();
                _query["status"] = request.Status.Select(_value => _value.Stringify()).ToList();
                if (request.OrderId != null)
                {
                    _query["orderId"] = request.OrderId;
                }
                if (request.NameOnAccount != null)
                {
                    _query["nameOnAccount"] = request.NameOnAccount;
                }
                if (request.Last4 != null)
                {
                    _query["last4"] = request.Last4;
                }
                if (request.DateFrom != null)
                {
                    _query["dateFrom"] = request.DateFrom.Value.ToString(Constants.DateTimeFormat);
                }
                if (request.DateTo != null)
                {
                    _query["dateTo"] = request.DateTo.Value.ToString(Constants.DateTimeFormat);
                }
                if (request.SettlementState != null)
                {
                    _query["settlementState"] = request.SettlementState.Value.Stringify();
                }
                if (request.SettlementDate != null)
                {
                    _query["settlementDate"] = request.SettlementDate.Value.ToString(
                        Constants.DateFormat
                    );
                }
                if (request.PaymentLinkId != null)
                {
                    _query["paymentLinkId"] = request.PaymentLinkId;
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
                        Path = "bank-transfer-payments",
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
    /// -	[Retrieve payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve) - View the details of the bank transfer payment.
    /// -	[Reverse payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/reverse) - Cancel the bank transfer payment if it's an open batch.
    /// -	[Refund payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/refund) - Run a referenced refund to return funds to the customer's bank account.
    ///
    /// **Payment methods**
    ///
    /// Our gateway accepts the following payment methods:
    /// -	Automated clearing house (ACH) details
    /// -	Pre-authorized debit (PAD) details
    ///
    /// You can also use [secure tokens](https://docs.payroc.com/api/schema/payments/secure-tokens/overview) and [single-use tokens](https://docs.payroc.com/api/schema/payments/single-use-tokens/create) that you created from ACH details or PAD details.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferPayments.CreateAsync(
    ///     new BankTransferPaymentRequest
    ///     {
    ///         IdempotencyKey = "Idempotency-Key",
    ///         ProcessingTerminalId = "1234001",
    ///         Order = new BankTransferPaymentOrder
    ///         {
    ///             OrderId = "OrderRef6543",
    ///             Description = "Large Pepperoni Pizza",
    ///             Amount = 4999,
    ///             Currency = Currency.Usd,
    ///             Breakdown = new BankTransferBreakdown
    ///             {
    ///                 Subtotal = 4347,
    ///                 Tip = new Tip { Type = TipType.Percentage, Percentage = 10 },
    ///                 Taxes = new List&lt;Tax&gt;()
    ///                 {
    ///                     new Tax { Name = "Sales Tax", Rate = 5 },
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
    ///             new BankTransferPaymentRequestPaymentMethod.Ach(
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
    public async Task<BankTransferPayment> CreateAsync(
        BankTransferPaymentRequest request,
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
                        return JsonUtils.Deserialize<BankTransferPayment>(responseBody)!;
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
    /// Use this method to retrieve information about a bank transfer payment.
    ///
    /// To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/create) method.
    ///
    /// Note: If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/list) method to search for the payment.
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
    /// await client.Payments.BankTransferPayments.RetrieveAsync(
    ///     new RetrieveBankTransferPaymentsRequest { PaymentId = "paymentId" }
    /// );
    /// </code></example>
    public async Task<BankTransferPayment> RetrieveAsync(
        RetrieveBankTransferPaymentsRequest request,
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
                                "bank-transfer-payments/{0}",
                                ValueConvert.ToPathParameterString(request.PaymentId)
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
                        return JsonUtils.Deserialize<BankTransferPayment>(responseBody)!;
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
    /// Use this method to cancel a bank transfer payment in an open batch. This is also known as voiding a payment.
    ///
    /// To cancel a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/list) method to search for the bank transfer payment.
    ///
    /// If your request is successful, our gateway removes the bank transfer payment from the merchant’s open batch and no funds are taken from the customer's bank account.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferPayments.ReverseAsync(
    ///     new ReverseBankTransferPaymentsRequest
    ///     {
    ///         PaymentId = "paymentId",
    ///         IdempotencyKey = "Idempotency-Key",
    ///     }
    /// );
    /// </code></example>
    public async Task<BankTransferPayment> ReverseAsync(
        ReverseBankTransferPaymentsRequest request,
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
                                "bank-transfer-payments/{0}/reverse",
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
                        return JsonUtils.Deserialize<BankTransferPayment>(responseBody)!;
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
    /// Use this method to refund a bank transfer payment that is in a closed batch.
    ///
    /// To refund a bank transfer payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/create) method.
    ///
    /// **Note:** If you don’t have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/list) method to search for the bank transfer payment.
    ///
    /// If your refund is successful, our gateway returns the payment amount to the customer's account.
    ///
    /// **Things to consider**
    /// - If the merchant refunds a bank transfer payment that is in an open batch, our gateway reverses the bank transfer payment.
    /// - Some merchants can run unreferenced refunds, which means that they don’t need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/payments/bank-transfer-refunds/create).
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferPayments.RefundAsync(
    ///     new BankTransferReferencedRefund
    ///     {
    ///         PaymentId = "paymentId",
    ///         IdempotencyKey = "Idempotency-Key",
    ///         Amount = 4999,
    ///         Description = "amount to refund",
    ///     }
    /// );
    /// </code></example>
    public async Task<BankTransferPayment> RefundAsync(
        BankTransferReferencedRefund request,
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
                                "bank-transfer-payments/{0}/refund",
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
                        return JsonUtils.Deserialize<BankTransferPayment>(responseBody)!;
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
    /// Use this method to re-present an ACH payment.
    ///
    /// To re-present a payment, you need the paymentId of the return. To get the paymentId of the return, complete the following steps:
    ///
    /// 1.	Use our [Retrieve Payment](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve) method  to view the details of the original payment.
    /// 2.	From the [returns object](https://docs.payroc.com/api/schema/payments/bank-transfer-payments/retrieve#response.body.returns) in the response, get the paymentId of the return.
    ///
    /// Our gateway uses the bank account details from the original payment. If you want to update the customer's bank account details, send the new bank account details in the request.
    ///
    /// If your request is successful, our gateway re-presents the payment.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferPayments.RepresentAsync(
    ///     new Representment
    ///     {
    ///         PaymentId = "paymentId",
    ///         IdempotencyKey = "Idempotency-Key",
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
    public async Task<BankTransferPayment> RepresentAsync(
        Representment request,
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
                        return JsonUtils.Deserialize<BankTransferPayment>(responseBody)!;
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
