using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.SecureTokens;

public partial class SecureTokensClient
{
    private RawClient _client;

    internal SecureTokensClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Return a list of secure tokens that are currently saved on the terminal.
    /// </summary>
    /// <example><code>
    /// await client.Payments.SecureTokens.ListAsync(
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
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<SecureToken>> ListAsync(
        ListSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _query = new Dictionary<string, object>();
                if (request.SecureTokenId != null)
                {
                    _query["secureTokenId"] = request.SecureTokenId;
                }
                if (request.CustomerName != null)
                {
                    _query["customerName"] = request.CustomerName;
                }
                if (request.Phone != null)
                {
                    _query["phone"] = request.Phone;
                }
                if (request.Email != null)
                {
                    _query["email"] = request.Email;
                }
                if (request.Token != null)
                {
                    _query["token"] = request.Token;
                }
                if (request.First6 != null)
                {
                    _query["first6"] = request.First6;
                }
                if (request.Last4 != null)
                {
                    _query["last4"] = request.Last4;
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
                    new RawClient.JsonApiRequest
                    {
                        BaseUrl = _client.Options.BaseUrl,
                        Method = HttpMethod.Get,
                        Path =
                            $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/secure-tokens",
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
                    .CreateAsync<SecureToken>(sendRequest, httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Save the customer's payment details to use in future transactions.
    /// </summary>
    /// <example><code>
    /// await client.Payments.SecureTokens.CreateAsync(
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
    ///             DateOfBirth = "1990-07-15",
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
    ///             new TokenizationRequestSource.Card(
    ///                 new CardPayload
    ///                 {
    ///                     CardDetails = new CardPayloadCardDetails(
    ///                         new CardPayloadCardDetails.Raw(
    ///                             new RawCardDetails
    ///                             {
    ///                                 Device = new Device
    ///                                 {
    ///                                     Model = DeviceModel.BbposChp,
    ///                                     SerialNumber = "PAX123456789",
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
    public async Task<SecureToken> CreateAsync(
        TokenizationRequest request,
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
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/secure-tokens",
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
                        return JsonUtils.Deserialize<SecureToken>(responseBody)!;
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
    /// Return a secure token and its related payment details.
    /// </summary>
    /// <example><code>
    /// await client.Payments.SecureTokens.GetAsync(
    ///     new GetSecureTokensRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    ///     }
    /// );
    /// </code></example>
    public async Task<SecureToken> GetAsync(
        GetSecureTokensRequest request,
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
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/secure-tokens/{JsonUtils.SerializeAsString(request.SecureTokenId)}",
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
                        return JsonUtils.Deserialize<SecureToken>(responseBody)!;
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
    /// Delete a secure token and its represented payment details.
    /// **Note**: If you delete a token, you can't reuse its identifier.
    /// </summary>
    /// <example><code>
    /// await client.Payments.SecureTokens.DeleteAsync(
    ///     new DeleteSecureTokensRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    ///     }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task DeleteAsync(
        DeleteSecureTokensRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var response = await _client
                    .SendRequestAsync(
                        new RawClient.JsonApiRequest
                        {
                            BaseUrl = _client.Options.BaseUrl,
                            Method = HttpMethod.Delete,
                            Path =
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/secure-tokens/{JsonUtils.SerializeAsString(request.SecureTokenId)}",
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
    /// Update the customer's payment details that are represented by the secure token.
    ///
    /// Structure your request to follow the RFC 6902 standard.
    /// </summary>
    /// <example><code>
    /// await client.Payments.SecureTokens.UpdateAsync(
    ///     new UpdateSecureTokensRequest
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
    public async Task<SecureToken> UpdateAsync(
        UpdateSecureTokensRequest request,
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
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/secure-tokens/{JsonUtils.SerializeAsString(request.SecureTokenId)}",
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
                        return JsonUtils.Deserialize<SecureToken>(responseBody)!;
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
    /// If you have a single-use token, use this method to update payment details that are represented by a secure token.
    ///
    /// If you don’t have a single-use token, and you want to update payment details represented by a secure token, go to
    /// [updateSecureToken](https://docs.payroc.com/api/resources#updateSecureToken).
    ///
    /// **Note**: For more information about tokenization, go to [tokenization](https://docs.payroc.com/knowledge/basic-concepts/tokenization).
    /// </summary>
    /// <example><code>
    /// await client.Payments.SecureTokens.AccountUpdateAsync(
    ///     new AccountUpdateSecureTokensRequest
    ///     {
    ///         SecureTokenId = "MREF_abc1de23-f4a5-6789-bcd0-12e345678901fa",
    ///         ProcessingTerminalId = "1234001",
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
    public async Task<SecureToken> AccountUpdateAsync(
        AccountUpdateSecureTokensRequest request,
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
                                $"processing-terminals/{JsonUtils.SerializeAsString(request.ProcessingTerminalId)}/secure-tokens/{JsonUtils.SerializeAsString(request.SecureTokenId)}/update-account",
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
                        return JsonUtils.Deserialize<SecureToken>(responseBody)!;
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
}
