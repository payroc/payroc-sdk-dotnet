using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingTerminals;

public partial class ProcessingTerminalsClient
{
    private RawClient _client;

    internal ProcessingTerminalsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to retrieve the configuration for a processing terminal. The configuration includes the terminal's settings and details about how our gateway should handle transactions.
    /// To get a processingTerminalId, go to [Retrieve Terminal Order](#getTerminalOrder) or [List Processing Terminals](#listProcessingAccountsProcessingTerminals).
    /// &gt; **Note**: If you want to retrieve the processor configuration for the processing terminal, go to [Retrieve Host Configuration](#getProcessingTerminalHostConfiguration).
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingTerminals.RetrieveAsync(
    ///     new RetrieveProcessingTerminalsRequest { ProcessingTerminalId = "processingTerminalId" }
    /// );
    /// </code></example>
    public async Task<ProcessingTerminal> RetrieveAsync(
        RetrieveProcessingTerminalsRequest request,
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
                                "processing-terminals/{0}",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId)
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
                        return JsonUtils.Deserialize<ProcessingTerminal>(responseBody)!;
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
    /// Use this method to retrieve the host configuration of a processing terminal. You need this information to connect a third-party gateway to a merchant account.
    /// &gt; **Note**: If you need information about a merchant or a list of processing accounts associated with a merchant's account, go to [Retrieve Merchant Platform](#getMerchantAcccounts).
    /// </summary>
    /// <example><code>
    /// await client.Boarding.ProcessingTerminals.RetrieveHostConfigurationAsync(
    ///     new RetrieveHostConfigurationProcessingTerminalsRequest
    ///     {
    ///         ProcessingTerminalId = "processingTerminalId",
    ///     }
    /// );
    /// </code></example>
    public async Task<HostConfiguration> RetrieveHostConfigurationAsync(
        RetrieveHostConfigurationProcessingTerminalsRequest request,
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
                                "processing-terminals/{0}/host-configurations",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId)
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
                        return JsonUtils.Deserialize<HostConfiguration>(responseBody)!;
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
