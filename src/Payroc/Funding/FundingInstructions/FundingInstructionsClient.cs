using System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.Funding.FundingInstructions;

public partial class FundingInstructionsClient : IFundingInstructionsClient
{
    private RawClient _client;

    internal FundingInstructionsClient(RawClient client)
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

    private async Task<WithRawResponse<Instruction>> CreateAsyncCore(
        CreateFundingInstructionsRequest request,
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
                            Path = "funding-instructions",
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
                        var responseData = JsonUtils.Deserialize<Instruction>(responseBody)!;
                        return new WithRawResponse<Instruction>()
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

    private async Task<WithRawResponse<Instruction>> RetrieveAsyncCore(
        RetrieveFundingInstructionsRequest request,
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
                                "funding-instructions/{0}",
                                ValueConvert.ToPathParameterString(request.InstructionId)
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
                        var responseData = JsonUtils.Deserialize<Instruction>(responseBody)!;
                        return new WithRawResponse<Instruction>()
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

    /// <summary>
    /// &gt; Important: You can return a list of funding instructions from only the previous two years. If you want to view a funding instruction from more than two years ago and you have its instructionId, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Use this method to return a [paginated](https://docs.payroc.com/api/pagination) list of funding instructions within a specific date range.
    ///
    /// **Note:** If you want to view the details of a specific funding instruction and you have its instructionId, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Our gateway returns the following information for each instruction in the list:
    /// -	Status of the funding instruction.
    /// -	Funding information, including which merchant's funding balance we distribute and the funding account that we send the balance to.
    ///
    /// For each funding instruction, we also return the instructionId, which you can use to perform follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Funding.FundingInstructions.ListAsync(
    ///     new ListFundingInstructionsRequest
    ///     {
    ///         Before = "2571",
    ///         After = "8516",
    ///         Limit = 1,
    ///         DateFrom = new DateOnly(2024, 7, 1),
    ///         DateTo = new DateOnly(2024, 7, 3),
    ///     }
    /// );
    /// </code></example>
    public async Task<PayrocPager<ListFundingInstructionsResponseDataItem>> ListAsync(
        ListFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return await _client
            .Options.ExceptionHandler.TryCatchAsync(async () =>
            {
                var _queryString = new Payroc.Core.QueryStringBuilder.Builder(capacity: 5)
                    .Add("before", request.Before)
                    .Add("after", request.After)
                    .Add("limit", request.Limit)
                    .Add("dateFrom", request.DateFrom)
                    .Add("dateTo", request.DateTo)
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
                        Path = "funding-instructions",
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
                    .CreateAsync<ListFundingInstructionsResponseDataItem>(
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
    /// Use this method to create a funding instruction that tells us how to distribute the funds from your merchants' transactions.
    ///
    /// **Note:** Before you create a funding instruction, you can use our [List Funding Balances](https://docs.payroc.com/api/schema/funding/funding-activity/retrieve-balance) method to view the amount of available funds that a merchant has.
    ///
    /// In your request, include an array of merchantInstruction objects. Each merchantInstruction object contains the following:
    /// -	Merchant ID (MID) of the merchant whose funding balance you want to distribute.
    /// -	Funding account that you want to send funds to.
    /// -	Amount that you want to send to the funding account.
    ///
    /// Our gateway returns the instructionId, which you can use to run follow-on actions.
    /// </summary>
    /// <example><code>
    /// await client.Funding.FundingInstructions.CreateAsync(
    ///     new CreateFundingInstructionsRequest
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new Instruction
    ///         {
    ///             Merchants = new List&lt;InstructionMerchantsItem&gt;()
    ///             {
    ///                 new InstructionMerchantsItem
    ///                 {
    ///                     MerchantId = "4525644354",
    ///                     Recipients = new List&lt;InstructionMerchantsItemRecipientsItem&gt;()
    ///                     {
    ///                         new InstructionMerchantsItemRecipientsItem
    ///                         {
    ///                             FundingAccountId = 123,
    ///                             PaymentMethod = InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
    ///                             Amount = new InstructionMerchantsItemRecipientsItemAmount
    ///                             {
    ///                                 Value = 120000,
    ///                                 Currency = InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
    ///                             },
    ///                             Metadata = new Dictionary&lt;string, string&gt;()
    ///                             {
    ///                                 { "yourCustomField", "abc123" },
    ///                             },
    ///                         },
    ///                     },
    ///                 },
    ///             },
    ///             Metadata = new Dictionary&lt;string, string&gt;() { { "yourCustomField", "abc123" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public WithRawResponseTask<Instruction> CreateAsync(
        CreateFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Instruction>(
            CreateAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// Use this method to retrieve information about a funding instruction.
    ///
    /// To retrieve a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.
    ///
    /// **Note:** If you don't have the instructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.
    ///
    /// Our gateway returns the following information about the funding instruction:
    /// -	Status of the funding instruction.
    /// -	Funding information, including which merchant's funding balance we distribute and the funding account that we send the balance to.
    /// </summary>
    /// <example><code>
    /// await client.Funding.FundingInstructions.RetrieveAsync(
    ///     new RetrieveFundingInstructionsRequest { InstructionId = 1 }
    /// );
    /// </code></example>
    public WithRawResponseTask<Instruction> RetrieveAsync(
        RetrieveFundingInstructionsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<Instruction>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }

    /// <summary>
    /// &gt; **Important:** You can update a funding instruction only if its status is `accepted`. To view the status of a funding instruction, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Use this method to update the details of a funding instruction.
    ///
    /// To update a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.
    ///
    /// **Note:** If you don't have the fundingInstructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.
    ///
    /// You can modify the following information for the funding instruction:
    /// -	Merchant ID (MID) of the merchant whose funding balance you want to distribute.
    /// -	Funding account that you want to send funds to.
    /// -	Amount that you want to send to the funding account.
    /// </summary>
    /// <example><code>
    /// await client.Funding.FundingInstructions.UpdateAsync(
    ///     new UpdateFundingInstructionsRequest
    ///     {
    ///         InstructionId = 1,
    ///         Body = new Instruction
    ///         {
    ///             Merchants = new List&lt;InstructionMerchantsItem&gt;()
    ///             {
    ///                 new InstructionMerchantsItem
    ///                 {
    ///                     MerchantId = "9876543219",
    ///                     Recipients = new List&lt;InstructionMerchantsItemRecipientsItem&gt;()
    ///                     {
    ///                         new InstructionMerchantsItemRecipientsItem
    ///                         {
    ///                             FundingAccountId = 124,
    ///                             PaymentMethod = InstructionMerchantsItemRecipientsItemPaymentMethod.Ach,
    ///                             Amount = new InstructionMerchantsItemRecipientsItemAmount
    ///                             {
    ///                                 Value = 69950,
    ///                                 Currency = InstructionMerchantsItemRecipientsItemAmountCurrency.Usd,
    ///                             },
    ///                             Metadata = new Dictionary&lt;string, string&gt;()
    ///                             {
    ///                                 { "supplier", "IT Support Services" },
    ///                             },
    ///                         },
    ///                     },
    ///                 },
    ///             },
    ///             Metadata = new Dictionary&lt;string, string&gt;() { { "instructionCreatedBy", "Jane Doe" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task UpdateAsync(
        UpdateFundingInstructionsRequest request,
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
                            Method = HttpMethod.Put,
                            Path = string.Format(
                                "funding-instructions/{0}",
                                ValueConvert.ToPathParameterString(request.InstructionId)
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
    /// &gt; **Important:** You can delete a funding instruction only if its status is `accepted`. To view the status of a funding instruction, use our [Retrieve Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/retrieve) method.
    ///
    /// Use this method to delete a funding instruction.
    ///
    /// To delete a funding instruction, you need its instructionId. Our gateway returned the instructionId in the response of the [Create Funding Instruction](https://docs.payroc.com/api/schema/funding/funding-instructions/create) method.
    ///
    /// **Note:** If you don't have the instructionId, use our [List Funding Instructions](https://docs.payroc.com/api/schema/funding/funding-instructions/list) method to search for the funding instruction.
    /// </summary>
    /// <example><code>
    /// await client.Funding.FundingInstructions.DeleteAsync(
    ///     new DeleteFundingInstructionsRequest { InstructionId = 1 }
    /// );
    /// </code></example>
    public async Task DeleteAsync(
        DeleteFundingInstructionsRequest request,
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
                                "funding-instructions/{0}",
                                ValueConvert.ToPathParameterString(request.InstructionId)
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
}
