using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;
using Payroc.Payments.ApplePaySessions;
using Payroc.Payments.BankAccounts;
using Payroc.Payments.BankTransferPayments;
using Payroc.Payments.BankTransferRefunds;
using Payroc.Payments.Cards;
using Payroc.Payments.CurrencyConversion;
using Payroc.Payments.HostedFields;
using Payroc.Payments.PaymentLinks;
using Payroc.Payments.PaymentPlans;
using Payroc.Payments.Refunds;
using Payroc.Payments.SecureTokens;
using Payroc.Payments.SingleUseTokens;
using Payroc.Payments.Subscriptions;

namespace Payroc.Payments;

public partial class PaymentsClient
{
    private RawClient _client;

    internal PaymentsClient(RawClient client)
    {
        _client = client;
        PaymentLinks = new PaymentLinksClient(_client);
        PaymentPlans = new PaymentPlansClient(_client);
        Subscriptions = new SubscriptionsClient(_client);
        SecureTokens = new SecureTokensClient(_client);
        SingleUseTokens = new SingleUseTokensClient(_client);
        HostedFields = new HostedFieldsClient(_client);
        ApplePaySessions = new ApplePaySessionsClient(_client);
        Refunds = new RefundsClient(_client);
        Cards = new CardsClient(_client);
        CurrencyConversion = new CurrencyConversionClient(_client);
        BankTransferPayments = new BankTransferPaymentsClient(_client);
        BankTransferRefunds = new BankTransferRefundsClient(_client);
        BankAccounts = new BankAccountsClient(_client);
    }

    public PaymentLinksClient PaymentLinks { get; }

    public PaymentPlansClient PaymentPlans { get; }

    public SubscriptionsClient Subscriptions { get; }

    public SecureTokensClient SecureTokens { get; }

    public SingleUseTokensClient SingleUseTokens { get; }

    public HostedFieldsClient HostedFields { get; }

    public ApplePaySessionsClient ApplePaySessions { get; }

    public RefundsClient Refunds { get; }

    public CardsClient Cards { get; }

    public CurrencyConversionClient CurrencyConversion { get; }

    public BankTransferPaymentsClient BankTransferPayments { get; }

    public BankTransferRefundsClient BankTransferRefunds { get; }

    public BankAccountsClient BankAccounts { get; }

    /// <summary>
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of payments.
    ///
    /// **Note:** If you want to view a specific payment and you have its paymentId, use our [Retrieve Payment](https://docs.payroc.com/api/schema/payments/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for payments for a customer, a tip mode, or a date range.
    ///
    /// Our gateway returns the following information about each payment in the list:
    ///
    /// - Order details, including the transaction amount and when it was processed.
    /// - Payment card details, including the masked card number, expiry date, and payment method.
    /// - Cardholder details, including their contact information and shipping address.
    /// - Payment details, including the payment type, status, and response.
    ///
    /// For each transaction, we also return the paymentId and an optional secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Payments.ListAsync(
    ///     new ListPaymentsRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         OrderId = "OrderRef6543",
    ///         Operator = "Jane",
    ///         CardholderName = "Sarah%20Hazel%20Hopper",
    ///         First6 = "453985",
    ///         Last4 = "7062",
    ///         DateFrom = new DateTime(2024, 07, 01, 15, 30, 00, 000),
    ///         DateTo = new DateTime(2024, 07, 03, 15, 30, 00, 000),
    ///         SettlementDate = new DateOnly(2024, 7, 2),
    ///         PaymentLinkId = "JZURRJBUPS",
    ///         Before = "2571",
    ///         After = "8516",
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<RetrievedPayment>> ListAsync(
        ListPaymentsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                _query["tipMode"] = request.TipMode.Select(_value => _value.Stringify()).ToList();
                _query["type"] = request.Type.Select(_value => _value.Stringify()).ToList();
                _query["status"] = request.Status.Select(_value => _value.Stringify()).ToList();
                if (request.ProcessingTerminalId != null)
                {
                    _query["processingTerminalId"] = request.ProcessingTerminalId;
                }
                if (request.OrderId != null)
                {
                    _query["orderId"] = request.OrderId;
                }
                if (request.Operator != null)
                {
                    _query["operator"] = request.Operator;
                }
                if (request.CardholderName != null)
                {
                    _query["cardholderName"] = request.CardholderName;
                }
                if (request.First6 != null)
                {
                    _query["first6"] = request.First6;
                }
                if (request.Last4 != null)
                {
                    _query["last4"] = request.Last4;
                }
                if (request.Tender != null)
                {
                    _query["tender"] = request.Tender.Value.Stringify();
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
                        Path = "payments",
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
                    .CreateAsync<RetrievedPayment>(
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
    /// Use this method to run a sale or a pre-authorization with a customer's payment card.
    ///
    /// In the response, our gateway returns information about the card payment and a paymentId, which you need for the following methods:
    ///
    /// -	[Retrieve payment](https://docs.payroc.com/api/schema/payments/retrieve) - View the details of the card payment.
    /// -	[Adjust payment](https://docs.payroc.com/api/schema/payments/adjust) - Update the details of the card payment.
    /// -	[Capture payment](https://docs.payroc.com/api/schema/payments/capture)  - Capture the pre-authorization.
    /// -	[Reverse payment](https://docs.payroc.com/api/schema/payments/reverse)  - Cancel the card payment if it's in an open batch.
    /// -	[Refund payment](https://docs.payroc.com/api/schema/payments/refund)  - Run a referenced refund to return funds to the payment card.
    ///
    /// **Payment methods**
    ///
    /// - **Cards** - Credit, debit, and EBT
    /// - **Digital wallets** - [Apple Pay®](https://docs.payroc.com/guides/integrate/apple-pay) and [Google Pay®](https://docs.payroc.com/guides/integrate/google-pay)
    /// - **Tokens** - Secure tokens and single-use tokens
    ///
    /// **Features**
    ///
    /// Our Create Payment method also supports the following features:
    ///
    /// - [Repeat payments](https://docs.payroc.com/guides/integrate/repeat-payments/use-your-own-software) - Run multiple payments as part of a payment schedule that you manage with your own software.
    /// - **Offline sales** - Run a sale or a pre-authorization if the terminal loses its connection to our gateway.
    /// - [Tokenization](https://docs.payroc.com/guides/integrate/save-payment-details) - Save card details to use in future transactions.
    /// - [3-D Secure](https://docs.payroc.com/guides/integrate/3-d-secure) - Verify the identity of the cardholder.
    /// - [Custom fields](https://docs.payroc.com/guides/integrate/add-custom-fields) - Add your own data to a payment.
    /// - **Tips** - Add tips to the card payment.
    /// - **Taxes** - Add local taxes to the card payment.
    /// - **Surcharging** - Add a surcharge to the card payment.
    /// - **Dual pricing** - Offer different prices based on payment method, for example, if you use our RewardPay Choice pricing program.
    /// </summary>
    /// <example><code>
    /// await client.Payments.CreateAsync(
    ///     new PaymentRequest
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Channel = PaymentRequestChannel.Web,
    ///         ProcessingTerminalId = "1234001",
    ///         Operator = "Jane",
    ///         Order = new PaymentOrder
    ///         {
    ///             OrderId = "OrderRef6543",
    ///             Description = "Large Pepperoni Pizza",
    ///             Amount = 4999,
    ///             Currency = Currency.Usd,
    ///         },
    ///         Customer = new Customer
    ///         {
    ///             FirstName = "Sarah",
    ///             LastName = "Hopper",
    ///             BillingAddress = new Address
    ///             {
    ///                 Address1 = "1 Example Ave.",
    ///                 Address2 = "Example Address Line 2",
    ///                 Address3 = "Example Address Line 3",
    ///                 City = "Chicago",
    ///                 State = "Illinois",
    ///                 Country = "US",
    ///                 PostalCode = "60056",
    ///             },
    ///             ShippingAddress = new Shipping
    ///             {
    ///                 RecipientName = "Sarah Hopper",
    ///                 Address = new Address
    ///                 {
    ///                     Address1 = "1 Example Ave.",
    ///                     Address2 = "Example Address Line 2",
    ///                     Address3 = "Example Address Line 3",
    ///                     City = "Chicago",
    ///                     State = "Illinois",
    ///                     Country = "US",
    ///                     PostalCode = "60056",
    ///                 },
    ///             },
    ///         },
    ///         PaymentMethod = new PaymentRequestPaymentMethod(
    ///             new PaymentRequestPaymentMethod.Card(
    ///                 new CardPayload
    ///                 {
    ///                     CardDetails = new CardPayloadCardDetails(
    ///                         new CardPayloadCardDetails.Raw(
    ///                             new RawCardDetails
    ///                             {
    ///                                 Device = new Device
    ///                                 {
    ///                                     Model = DeviceModel.BbposChp,
    ///                                     SerialNumber = "1850010868",
    ///                                 },
    ///                                 RawData =
    ///                                     "A1B2C3D4E5F67890ABCD1234567890ABCDEF1234567890ABCDEF1234567890ABCDEF",
    ///                             }
    ///                         )
    ///                     ),
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
    public async Task<Payment> CreateAsync(
        PaymentRequest request,
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
                            Path = "payments",
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
                        return JsonUtils.Deserialize<Payment>(responseBody)!;
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
    /// Use this method to retrieve information about a card payment.
    ///
    /// To retrieve a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.
    ///
    /// Our gateway returns the following information about the payment:
    ///
    /// - Order details, including the transaction amount and when it was processed.
    /// - Payment card details, including the masked card number, expiry date, and payment method.
    /// - Cardholder details, including their contact information and shipping address.
    /// - Payment details, including the payment type, status, and response.
    ///
    /// If the merchant saved the customer's card details, our gateway returns a secureTokenID, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Payments.RetrieveAsync(new RetrievePaymentsRequest { PaymentId = "M2MJOG6O2Y" });
    /// </code></example>
    public async Task<RetrievedPayment> RetrieveAsync(
        RetrievePaymentsRequest request,
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
                                "payments/{0}",
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
                        return JsonUtils.Deserialize<RetrievedPayment>(responseBody)!;
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
    /// Use this method to adjust a payment in an open batch.
    ///
    /// To adjust a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.
    ///
    /// You can adjust the following details of the payment:
    /// - Sale amount and tip amount
    /// - Payment status
    /// - Cardholder shipping address and contact information
    /// - Cardholder signature data
    ///
    /// Our gateway returns information about the adjusted payment, including information about the payment card and the cardholder.
    /// </summary>
    /// <example><code>
    /// await client.Payments.AdjustAsync(
    ///     new PaymentAdjustment
    ///     {
    ///         PaymentId = "M2MJOG6O2Y",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Adjustments = new List&lt;PaymentAdjustmentAdjustmentsItem&gt;()
    ///         {
    ///             new PaymentAdjustmentAdjustmentsItem(
    ///                 new PaymentAdjustmentAdjustmentsItem.Customer(new CustomerAdjustment())
    ///             ),
    ///             new PaymentAdjustmentAdjustmentsItem(
    ///                 new PaymentAdjustmentAdjustmentsItem.Order(new OrderAdjustment { Amount = 4999 })
    ///             ),
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<Payment> AdjustAsync(
        PaymentAdjustment request,
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
                                "payments/{0}/adjust",
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
                        return JsonUtils.Deserialize<Payment>(responseBody)!;
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
    /// Use this method to capture a pre-authorization.
    ///
    /// To capture a pre-authorization, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.
    ///
    /// Depending on the amount you want to capture, complete the following:
    /// -	**Capture the full amount of the pre-authorization** - Don't send a value for the amount parameter in your request.
    /// -	**Capture less than the amount of the pre-authorization** - Send a value for the amount parameter in your request.
    /// -	**Capture more than the amount of the pre-authorization** - Adjust the pre-authorization before you capture it. For more information about adjusting a pre-authorization, go to [Adjust Payment](https://docs.payroc.com/api/schema/payments/adjust).
    ///
    /// If your request is successful, our gateway takes the amount from the payment card.
    ///
    /// **Note:** For more information about pre-authorizations and captures, go to [Run a pre-authorization](https://docs.payroc.com/guides/integrate/run-a-pre-authorization).
    /// </summary>
    /// <example><code>
    /// await client.Payments.CaptureAsync(
    ///     new PaymentCapture
    ///     {
    ///         PaymentId = "M2MJOG6O2Y",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         ProcessingTerminalId = "1234001",
    ///         Operator = "Jane",
    ///         Amount = 4999,
    ///         Breakdown = new ItemizedBreakdown
    ///         {
    ///             Subtotal = 4999,
    ///             DutyAmount = 499,
    ///             FreightAmount = 500,
    ///             Items = new List&lt;LineItem&gt;()
    ///             {
    ///                 new LineItem { UnitPrice = 4000, Quantity = 1 },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<Payment> CaptureAsync(
        PaymentCapture request,
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
                                "payments/{0}/capture",
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
                        return JsonUtils.Deserialize<Payment>(responseBody)!;
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
    /// Use this method to cancel or to partially cancel a payment in an open batch. This is also known as voiding a payment.
    ///
    /// To cancel a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.
    ///
    /// If your request is successful, our gateway removes the payment from the merchant's open batch and no funds are taken from the cardholder's account.
    /// </summary>
    /// <example><code>
    /// await client.Payments.ReverseAsync(
    ///     new PaymentReversal
    ///     {
    ///         PaymentId = "M2MJOG6O2Y",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Amount = 4999,
    ///     }
    /// );
    /// </code></example>
    public async Task<Payment> ReverseAsync(
        PaymentReversal request,
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
                                "payments/{0}/reverse",
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
                        return JsonUtils.Deserialize<Payment>(responseBody)!;
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
    /// Use this method to refund a payment that is in a closed batch.
    ///
    /// To refund a payment, you need its paymentId. Our gateway returned the paymentId in the response of the [Create Payment](https://docs.payroc.com/api/schema/payments/create) method.
    ///
    /// **Note:** If you don't have the paymentId, use our [List Payments](https://docs.payroc.com/api/schema/payments/list) method to search for the payment.
    ///
    /// If your refund is successful, our gateway returns the payment amount to the cardholder's account.
    ///
    /// **Things to consider**
    ///
    /// - If the merchant refunds a payment that is in an open batch, our gateway reverses the payment.
    /// - Some merchants can run unreferenced refunds, which means that they don't need a paymentId to return an amount to a customer. For more information about how to run an unreferenced refund, go to [Create Refund](https://docs.payroc.com/api/schema/payments/refunds/create).
    /// </summary>
    /// <example><code>
    /// await client.Payments.RefundAsync(
    ///     new ReferencedRefund
    ///     {
    ///         PaymentId = "M2MJOG6O2Y",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Amount = 4999,
    ///         Description = "Refund for order OrderRef6543",
    ///     }
    /// );
    /// </code></example>
    public async Task<Payment> RefundAsync(
        ReferencedRefund request,
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
                                "payments/{0}/refund",
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
                        return JsonUtils.Deserialize<Payment>(responseBody)!;
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
}
