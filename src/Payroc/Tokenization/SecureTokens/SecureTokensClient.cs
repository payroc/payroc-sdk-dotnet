using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.Tokenization.SecureTokens;

public partial class SecureTokensClient : ISecureTokensClient
{
    private RawClient _client;

    internal SecureTokensClient(RawClient client)
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

    private async Task<WithRawResponse<SecureToken>> CreateAsyncCore(
        TokenizationRequest request,
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
                                "processing-terminals/{0}/secure-tokens",
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
                        var responseData = JsonUtils.Deserialize<SecureToken>(responseBody)!;
                        return new WithRawResponse<SecureToken>()
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

    private async Task<WithRawResponse<SecureTokenWithAccountType>> RetrieveAsyncCore(
        RetrieveSecureTokensRequest request,
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
                                "processing-terminals/{0}/secure-tokens/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SecureTokenId)
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
                        var responseData = JsonUtils.Deserialize<SecureTokenWithAccountType>(
                            responseBody
                        )!;
                        return new WithRawResponse<SecureTokenWithAccountType>()
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

    private async Task<WithRawResponse<SecureToken>> PartiallyUpdateAsyncCore(
        PartiallyUpdateSecureTokensRequest request,
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
                                "processing-terminals/{0}/secure-tokens/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SecureTokenId)
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
                        var responseData = JsonUtils.Deserialize<SecureToken>(responseBody)!;
                        return new WithRawResponse<SecureToken>()
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

    private async Task<WithRawResponse<SecureToken>> UpdateAccountAsyncCore(
        UpdateAccountSecureTokensRequest request,
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
                                "processing-terminals/{0}/secure-tokens/{1}/update-account",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SecureTokenId)
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
                        var responseData = JsonUtils.Deserialize<SecureToken>(responseBody)!;
                        return new WithRawResponse<SecureToken>()
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
                            case 413:
                                throw new ContentTooLargeError(
                                    JsonUtils.Deserialize<FourHundredThirteen>(responseBody)
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
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of secure tokens.
    ///
    /// **Note:** If you want to view the details of a specific secure token and you have its secureTokenId, use our [Retrieve Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/retrieve) method.
    ///
    /// Use query parameters to filter the list of results that we return, for example, to search for secure tokens by customer or by the first four digits of a card number.
    ///
    /// Our gateway returns information about the following for each secure token in the list:
    ///
    ///   -	Payment details that the secure token represents.
    ///   -	Customer details, including shipping and billing addresses.
    ///   -	Secure token that you can use to carry out transactions.
    ///
    ///   For each secure token, we also return the secureTokenId, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Tokenization.SecureTokens.ListAsync(
    ///     new ListSecureTokensRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    ///         CustomerName = "Sarah%20Hazel%20Hopper",
    ///         Phone = "2025550165",
    ///         Email = "sarah.hopper@example.com",
    ///         Token = "296753123456",
    ///         First6 = "453985",
    ///         Last4 = "7062",
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<SecureTokenWithAccountType>> ListAsync(
        ListSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 10)
                    .Add("secureTokenId", request.SecureTokenId)
                    .Add("customerName", request.CustomerName)
                    .Add("phone", request.Phone)
                    .Add("email", request.Email)
                    .Add("token", request.Token)
                    .Add("first6", request.First6)
                    .Add("last4", request.Last4)
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
                            "processing-terminals/{0}/secure-tokens",
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
                    .CreateAsync<SecureTokenWithAccountType>(
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
    /// Use this method to create a secure token that represents a customer's payment details.
    ///
    /// When you create a secure token, you need to generate and provide a secureTokenId that you use to run follow-on actions:
    /// - [Retrieve Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/retrieve) – View the details of the secure token.
    /// - [Delete Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/delete) – Delete the secure token.
    /// - [Update Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/partially-update) – Update the details of the secure token.
    /// - [Update Account Details](https://docs.payroc.com/api/schema/tokenization/secure-tokens/update-account) – Update the secure token with the details from a single-use token.
    ///
    /// **Note:** If you don't generate a secureTokenId to identify the token, our gateway generates a unique identifier and returns it in the response.
    ///
    /// If the request is successful, our gateway returns a token that the merchant can use in transactions instead of the customer's sensitive payment details, for example, when they [run a sale](https://docs.payroc.com/api/schema/card-payments/payments/create).
    /// </summary>
    /// <example><code>
    /// await client.Tokenization.SecureTokens.CreateAsync(
    ///     new TokenizationRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Operator = "Jane",
    ///         MitAgreement = TokenizationRequestMitAgreement.Unscheduled,
    ///         Customer = new Customer
    ///         {
    ///             FirstName = "Sarah",
    ///             LastName = "Hopper",
    ///             DateOfBirth = new DateOnly(1990, 7, 15),
    ///             ReferenceNumber = "Customer-12",
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
    ///             ContactMethods = new List&lt;ContactMethod&gt;()
    ///             {
    ///                 new ContactMethod(
    ///                     new ContactMethod.Email(
    ///                         new ContactMethodEmail { Value = "jane.doe@example.com" }
    ///                     )
    ///                 ),
    ///             },
    ///             NotificationLanguage = CustomerNotificationLanguage.En,
    ///         },
    ///         IpAddress = new IpAddress { Type = IpAddressType.Ipv4, Value = "104.18.24.203" },
    ///         Source = new TokenizationRequestSource(
    ///             new Payroc.Tokenization.SecureTokens.TokenizationRequestSource.Card(
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
    public WithRawResponseTask<SecureToken> CreateAsync(
        TokenizationRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<SecureToken>(
            CreateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to retrieve information about a secure token.
    ///
    /// To retrieve a secure token, you need its secureTokenID, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.
    ///
    /// **Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search for the secure token.
    ///
    /// Our gateway returns the following information about the secure token:
    ///
    ///   -	Payment details that the secure token represents.
    ///   -	Customer details, including shipping and billing addresses.
    ///   -	Secure token that you can use to carry out transactions.
    /// </summary>
    /// <example><code>
    /// await client.Tokenization.SecureTokens.RetrieveAsync(
    ///     new RetrieveSecureTokensRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<SecureTokenWithAccountType> RetrieveAsync(
        RetrieveSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<SecureTokenWithAccountType>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to delete a secure token and its related payment details from our vault.
    ///
    /// To delete a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.
    ///
    /// **Note:** If you don’t have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search for the secure token.
    ///
    /// When you delete a secure token, you can’t recover it, and you can’t reuse its identifier for a new token.
    /// </summary>
    /// <example><code>
    /// await client.Tokenization.SecureTokens.DeleteAsync(
    ///     new DeleteSecureTokensRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    ///     }
    /// );
    /// </code></example>
    public async Task DeleteAsync(
        DeleteSecureTokensRequest request,
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
                                "processing-terminals/{0}/secure-tokens/{1}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId),
                                ValueConvert.ToPathParameterString(request.SecureTokenId)
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
    /// Use this method to partially update a secure token. Structure your request to follow the [RFC 6902](https://datatracker.ietf.org/doc/html/rfc6902) standard.
    ///
    /// To update a secure token, you need its secureTokenId, which you sent in the request of the [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create) method.
    ///
    /// **Note:** If you don't have the secureTokenId, use our [List Secure Tokens](https://docs.payroc.com/api/schema/tokenization/secure-tokens/list) method to search  for the payment.
    ///
    /// You can update all of the properties of the secure token, except the following:
    /// - processingTerminalId
    /// - type
    /// - token
    /// - status
    /// - source/Card
    ///   - type
    ///   - cardNumber
    ///   - cardType
    ///   - currency
    ///   - debit
    ///   - surcharging
    /// - source/ACH account
    ///   - accountNumber
    ///   - routingNumber
    /// - source/PAD account
    ///   - type
    ///   - accountNumber
    ///   - transitNumber
    /// </summary>
    /// <example><code>
    /// await client.Tokenization.SecureTokens.PartiallyUpdateAsync(
    ///     new PartiallyUpdateSecureTokensRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
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
    public WithRawResponseTask<SecureToken> PartiallyUpdateAsync(
        PartiallyUpdateSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<SecureToken>(
            PartiallyUpdateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to update a secure token if you have a single-use token from Hosted Fields.
    ///
    /// **Note:** If you don't have a single-use token, you can update saved payment details with our [Update Secure Token](https://docs.payroc.com/api/resources#updateSecureToken) method. For more information about our two options to update a secure token, go to [Update saved payment details](https://docs.payroc.com/guides/take-payments/update-saved-payment-details).
    /// </summary>
    /// <example><code>
    /// await client.Tokenization.SecureTokens.UpdateAccountAsync(
    ///     new UpdateAccountSecureTokensRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new AccountUpdate(
    ///             new AccountUpdate.SingleUseToken(
    ///                 new SingleUseTokenAccountUpdate
    ///                 {
    ///                     Token =
    ///                         "abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890",
    ///                 }
    ///             )
    ///         ),
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<SecureToken> UpdateAccountAsync(
        UpdateAccountSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<SecureToken>(
            UpdateAccountAsyncCore(request, options, cancellationToken)
        );
    }
}
