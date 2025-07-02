using System.Net.Http;
using System.Text.Json;
using System.Threading;
using global::System.Threading.Tasks;
using OneOf;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.PricingIntents;

public partial class PricingIntentsClient
{
    private RawClient _client;

    internal PricingIntentsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Retrieve a list of pricing intents.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.PricingIntents.ListAsync(
    ///     new ListPricingIntentsRequest { Before = "2571", After = "8516" }
    /// );
    /// </code></example>
    public async Task<PayrocPager<OneOf<PricingIntent50>>> ListAsync(
        ListPricingIntentsRequest request,
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
                        Path = "pricing-intents",
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
                    .CreateAsync<OneOf<PricingIntent50>>(
                        sendRequest,
                        httpRequest,
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Create a pricing intent.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.PricingIntents.CreateAsync(
    ///     new CreatePricingIntentsRequest
    ///     {
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         Body = new PricingIntent50
    ///         {
    ///             Country = "US",
    ///             Version = "5.0",
    ///             Base = new BaseUs
    ///             {
    ///                 AddressVerification = 5,
    ///                 AnnualFee = new BaseUsAnnualFee
    ///                 {
    ///                     BillInMonth = BaseUsAnnualFeeBillInMonth.June,
    ///                     Amount = 9900,
    ///                 },
    ///                 RegulatoryAssistanceProgram = 15,
    ///                 PciNonCompliance = 4995,
    ///                 MerchantAdvantage = 10,
    ///                 PlatinumSecurity = new BaseUsPlatinumSecurity(
    ///                     new BaseUsPlatinumSecurity.Monthly(new BaseUsMonthly())
    ///                 ),
    ///                 Maintenance = 500,
    ///                 Minimum = 100,
    ///                 VoiceAuthorization = 95,
    ///                 Chargeback = 2500,
    ///                 Retrieval = 1500,
    ///                 Batch = 1500,
    ///                 EarlyTermination = 57500,
    ///             },
    ///             Processor = new PricingAgreementUs50Processor
    ///             {
    ///                 Card = new PricingAgreementUs50ProcessorCard(
    ///                     new PricingAgreementUs50ProcessorCard.InterchangePlus(
    ///                         new InterchangePlus
    ///                         {
    ///                             Fees = new InterchangePlusFees
    ///                             {
    ///                                 MastercardVisaDiscover = new ProcessorFee(),
    ///                             },
    ///                         }
    ///                     )
    ///                 ),
    ///             },
    ///             Services = new List&lt;ServiceUs50&gt;()
    ///             {
    ///                 new ServiceUs50(
    ///                     new ServiceUs50.HardwareAdvantagePlan(
    ///                         new HardwareAdvantagePlan { Enabled = true }
    ///                     )
    ///                 ),
    ///             },
    ///             Key = "Your-Unique-Identifier",
    ///             Metadata = new Dictionary&lt;string, string&gt;() { { "yourCustomField", "abc123" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<OneOf<PricingIntent50>> CreateAsync(
        CreatePricingIntentsRequest request,
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
                            Path = "pricing-intents",
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
                        return JsonUtils.Deserialize<OneOf<PricingIntent50>>(responseBody)!;
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
    /// Retrieve a specific pricing intent.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.PricingIntents.GetAsync(
    ///     new GetPricingIntentsRequest { PricingIntentId = "5" }
    /// );
    /// </code></example>
    public async Task<OneOf<PricingIntent50>> GetAsync(
        GetPricingIntentsRequest request,
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
                                "pricing-intents/{0}",
                                ValueConvert.ToPathParameterString(request.PricingIntentId)
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
                        return JsonUtils.Deserialize<OneOf<PricingIntent50>>(responseBody)!;
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
    /// Update a pricing intent.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.PricingIntents.UpdateAsync(
    ///     new UpdatePricingIntentsRequest
    ///     {
    ///         PricingIntentId = "5",
    ///         Body = new PricingIntent50
    ///         {
    ///             Country = "US",
    ///             Version = "5.0",
    ///             Base = new BaseUs
    ///             {
    ///                 AddressVerification = 5,
    ///                 AnnualFee = new BaseUsAnnualFee
    ///                 {
    ///                     BillInMonth = BaseUsAnnualFeeBillInMonth.June,
    ///                     Amount = 9900,
    ///                 },
    ///                 RegulatoryAssistanceProgram = 15,
    ///                 PciNonCompliance = 4995,
    ///                 MerchantAdvantage = 10,
    ///                 PlatinumSecurity = new BaseUsPlatinumSecurity(
    ///                     new BaseUsPlatinumSecurity.Monthly(new BaseUsMonthly())
    ///                 ),
    ///                 Maintenance = 500,
    ///                 Minimum = 100,
    ///                 VoiceAuthorization = 95,
    ///                 Chargeback = 2500,
    ///                 Retrieval = 1500,
    ///                 Batch = 1500,
    ///                 EarlyTermination = 57500,
    ///             },
    ///             Processor = new PricingAgreementUs50Processor
    ///             {
    ///                 Card = new PricingAgreementUs50ProcessorCard(
    ///                     new PricingAgreementUs50ProcessorCard.InterchangePlus(
    ///                         new InterchangePlus
    ///                         {
    ///                             Fees = new InterchangePlusFees
    ///                             {
    ///                                 MastercardVisaDiscover = new ProcessorFee(),
    ///                             },
    ///                         }
    ///                     )
    ///                 ),
    ///                 Ach = new Ach
    ///                 {
    ///                     Fees = new AchFees
    ///                     {
    ///                         Transaction = 50,
    ///                         Batch = 5,
    ///                         Returns = 400,
    ///                         UnauthorizedReturn = 1999,
    ///                         Statement = 800,
    ///                         MonthlyMinimum = 20000,
    ///                         AccountVerification = 10,
    ///                         DiscountRateUnder10000 = 5.25,
    ///                         DiscountRateAbove10000 = 10,
    ///                     },
    ///                 },
    ///             },
    ///             Gateway = new GatewayUs50
    ///             {
    ///                 Fees = new GatewayUs50Fees
    ///                 {
    ///                     Monthly = 2000,
    ///                     Setup = 5000,
    ///                     PerTransaction = 2000,
    ///                     PerDeviceMonthly = 10,
    ///                 },
    ///             },
    ///             Services = new List&lt;ServiceUs50&gt;()
    ///             {
    ///                 new ServiceUs50(
    ///                     new ServiceUs50.HardwareAdvantagePlan(
    ///                         new HardwareAdvantagePlan { Enabled = true }
    ///                     )
    ///                 ),
    ///             },
    ///             Key = "Your-Unique-Identifier",
    ///             Metadata = new Dictionary&lt;string, string&gt;() { { "yourCustomField", "abc123" } },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task UpdateAsync(
        UpdatePricingIntentsRequest request,
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
                            Method = HttpMethod.Put,
                            Path = string.Format(
                                "pricing-intents/{0}",
                                ValueConvert.ToPathParameterString(request.PricingIntentId)
                            ),
                            Body = request.Body,
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
    /// Delete a pricing intent.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.PricingIntents.DeleteAsync(
    ///     new DeletePricingIntentsRequest { PricingIntentId = "5" }
    /// );
    /// </code></example>
    public async global::System.Threading.Tasks.Task DeleteAsync(
        DeletePricingIntentsRequest request,
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
                                "pricing-intents/{0}",
                                ValueConvert.ToPathParameterString(request.PricingIntentId)
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
    /// Partially update an existing pricing intent.
    ///
    /// Structure your request to follow the RFC 6902 standard.
    /// </summary>
    /// <example><code>
    /// await client.Boarding.PricingIntents.PatchAsync(
    ///     new PatchPricingIntentsRequest
    ///     {
    ///         PricingIntentId = "5",
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
    public async Task<OneOf<PricingIntent50>> PatchAsync(
        PatchPricingIntentsRequest request,
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
                                "pricing-intents/{0}",
                                ValueConvert.ToPathParameterString(request.PricingIntentId)
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
                        return JsonUtils.Deserialize<OneOf<PricingIntent50>>(responseBody)!;
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
}
