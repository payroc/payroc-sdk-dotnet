using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Payments.HostedFields;

public partial class HostedFieldsClient
{
    private RawClient _client;

    internal HostedFieldsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Use this method to create a Hosted Fields session token. You need to generate a new session token each time you load Hosted Fields on a webpage.
    ///
    /// In your request, you need to indicate whether the merchant is using Hosted Fields to run a sale, save payment details, or update saved payment details.
    ///
    /// In the response, our gateway returns the session token and the time that it expires. You need the session token when you configure the JavaScript for Hosted Fields.
    ///
    /// For more information about adding Hosted Fields to a webpage, go to [Hosted Fields](https://docs.payroc.com/guides/integrate/hosted-fields).
    /// </summary>
    /// <example><code>
    /// await client.Payments.HostedFields.CreateAsync(
    ///     new HostedFieldsCreateSessionRequest
    ///     {
    ///         ProcessingTerminalId = "1234001",
    ///         IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
    ///         LibVersion = "1.1.0.123456",
    ///         Scenario = HostedFieldsCreateSessionRequestScenario.Payment,
    ///     }
    /// );
    /// </code></example>
    public async Task<HostedFieldsCreateSessionResponse> CreateAsync(
        HostedFieldsCreateSessionRequest request,
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
                                "processing-terminals/{0}/hosted-fields-sessions",
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
                        return JsonUtils.Deserialize<HostedFieldsCreateSessionResponse>(
                            responseBody
                        )!;
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
}
