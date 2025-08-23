using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.ApplePaySessions;

public partial class ApplePaySessionsClient
{
    private RawClient _client;

    internal ApplePaySessionsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to start an Apple Pay session for your merchant.
    ///
    /// In the response, we return the startSessionObject that you send to Apple when you retrieve the cardholder's encrypted payment details.
    ///
    /// **Note:** For more information about how to integrate with Apple Pay, go to [Apple Pay](https://docs.payroc.com/guides/integrate/apple-pay).
    /// </summary>
    /// <example><code>
    /// await client.Payments.ApplePaySessions.CreateAsync(
    ///     new ApplePaySessions
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         AppleDomainId = "DUHDZJHGYY",
    ///         AppleValidationUrl = "https://apple-pay-gateway.apple.com/paymentservices/startSession",
    ///     }
    /// );
    /// </code></example>
    public async Task<ApplePayResponseSession> CreateAsync(
        ApplePaySessions request,
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
                                "processing-terminals/{0}/apple-pay-sessions",
                                ValueConvert.ToPathParameterString(request.ProcessingTerminalId)
                            ),
                            Body = request,
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
                        return JsonUtils.Deserialize<ApplePayResponseSession>(responseBody)!;
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
