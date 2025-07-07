using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.BankTransferRefunds;

public partial class BankTransferRefundsClient
{
    private RawClient _client;

    internal BankTransferRefundsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to return a [paginated](/api/pagination) list of bank transfer refunds.
    ///
    /// **Note:** If you want to view a specific refund and you have its refundId, use our Retrieve Refund method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for refunds for a customer, an orderId, or a date range.
    ///
    /// Our gateway returns the following information about each refund in the list:
    ///
    /// -	Order details, including the refund amount and when it was processed.
    /// -	Bank account details, including the customer’s name and account number.
    ///
    /// For referenced refunds, our gateway also returns details about the payment that the refund is linked to.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferRefunds.ListAsync(
    ///     new ListBankTransferRefundsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         OrderId = "OrderRef6543",
    ///         NameOnAccount = "Sarah%20Hazel%20Hopper",
    ///         Last4 = "7062",
    ///         DateFrom = new DateTime(2024, 07, 01, 00, 00, 00, 000),
    ///         DateTo = new DateTime(2024, 07, 31, 23, 59, 59, 000),
    ///         SettlementDate = new DateOnly(2024, 7, 15),
    ///         Before = "2571",
    ///         After = "8516",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<BankTransferRefund>> ListAsync(
        ListBankTransferRefundsRequest request,
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
                        Path = "bank-transfer-refunds",
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
                    .CreateAsync<BankTransferRefund>(sendRequest, httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Use this method to create an unreferenced refund. An unreferenced refund is a refund that isn’t linked to a bank transfer payment.
    ///
    /// **Note:** If you have the paymentId of the payment you want to refund, use our [Refund Payment](/api/schema/payments/bank-transfer-payments/refund) method. If you use our Refund Payment method, our gateway sends the refund amount to the customer’s original payment method and links the refund to the payment.
    ///
    /// In the request, you must provide the customer’s payment method and information about the order including the refund amount.
    ///
    /// In the response, our gateway returns information about the refund and a refundId, which you need for the following methods:
    ///
    /// -	[Retrieve refund](/api/schema/payments/bank-transfer-refunds/get) – View the details of the refund.
    /// -	[Reverse refund](/api/schema/payments/bank-transfer-refunds/reverse) – Cancel the refund if it’s in an open batch.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferRefunds.CreateAsync(
    ///     new BankTransferUnreferencedRefund
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         ProcessingTerminalId = "1234001",
    ///         Order = new BankTransferRefundOrder
    ///         {
    ///             OrderId = "OrderRef6543",
    ///             Description = "Refund for order OrderRef6543",
    ///             Amount = 4999,
    ///             Currency = Currency.Usd,
    ///         },
    ///         RefundMethod = new BankTransferUnreferencedRefundRefundMethod(
    ///             new BankTransferUnreferencedRefundRefundMethod.Ach(
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
    public async Task<BankTransferRefund> CreateAsync(
        BankTransferUnreferencedRefund request,
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
                            Path = "bank-transfer-refunds",
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
                        return JsonUtils.Deserialize<BankTransferRefund>(responseBody)!;
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
    /// Use this method to retrieve information about a refund.
    ///
    /// To retrieve a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](/api/schema/payments/bank-transfer-payments/refund) method or the [Create Refund](/api/schema/payments/bank-transfer-refunds/create) method.
    ///
    /// **Note:** If you don’t have the refundId, use our [List Refunds](/api/schema/payments/bank-transfer-refunds/list) method to search for the refund.
    ///
    /// Our gateway returns the following information about the refund:
    ///
    /// - Order details, including the refund amount and when it was processed.
    /// - Bank account details, including the customer’s name and account number.
    ///
    /// If the refund is a referenced refund, our gateway also returns details about the payment that the refund is linked to.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferRefunds.RetrieveAsync(
    ///     new RetrieveBankTransferRefundsRequest { RefundId = "CD3HN88U9F" }
    /// );
    /// </code></example>
    public async Task<BankTransferRefund> RetrieveAsync(
        RetrieveBankTransferRefundsRequest request,
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
                                "bank-transfer-refunds/{0}",
                                ValueConvert.ToPathParameterString(request.RefundId)
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
                        return JsonUtils.Deserialize<BankTransferRefund>(responseBody)!;
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
    /// Use this method to cancel a bank transfer refund in an open batch.
    ///
    /// To cancel a refund, you need its refundId. Our gateway returned the refundId in the response of the [Refund Payment](/api/schema/payments/bank-transfer-payments/refund) or [Create Refund](/api/schema/payments/bank-transfer-refunds/create) method.
    ///
    /// **Note:** If you don’t have the refundId, use our [List Refunds](/api/schema/payments/bank-transfer-refunds/list) method to search for the refund.
    ///
    /// If your request is successful, the gateway removes the refund from the merchant’s open batch, and no funds are returned to the cardholder’s account.
    /// </summary>
    /// <example><code>
    /// await client.Payments.BankTransferRefunds.ReverseAsync(
    ///     new ReverseBankTransferRefundsRequest
    ///     {
    ///         RefundId = "CD3HN88U9F",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///     }
    /// );
    /// </code></example>
    public async Task<BankTransferRefund> ReverseAsync(
        ReverseBankTransferRefundsRequest request,
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
                                "bank-transfer-refunds/{0}/reverse",
                                ValueConvert.ToPathParameterString(request.RefundId)
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
                        return JsonUtils.Deserialize<BankTransferRefund>(responseBody)!;
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
