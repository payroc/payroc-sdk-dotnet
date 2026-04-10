using global::System.Text.Json;
using Payroc;
using Payroc.Core;

namespace Payroc.PayrocCloud.ClosedLoopReads;

public partial class ClosedLoopReadsClient : IClosedLoopReadsClient
{
    private readonly RawClient _client;

    internal ClosedLoopReadsClient(RawClient client)
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

    private async Task<WithRawResponse<ClosedLoopResponse>> RetrieveAsyncCore(
        RetrieveClosedLoopReadsRequest request,
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
                                "closed-loop-reads/{0}",
                                ValueConvert.ToPathParameterString(request.ClosedLoopReadId)
                            ),
                            Headers = _headers,
                            Options = options,
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response
                        .Raw.Content.ReadAsStringAsync(cancellationToken)
                        .ConfigureAwait(false);
                    try
                    {
                        var responseData = JsonUtils.Deserialize<ClosedLoopResponse>(responseBody)!;
                        return new WithRawResponse<ClosedLoopResponse>()
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
                    var responseBody = await response
                        .Raw.Content.ReadAsStringAsync(cancellationToken)
                        .ConfigureAwait(false);
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
    /// Use this method to retrieve information that a payment device captured from a closed-loop card.
    ///
    /// A closed-loop card is a type of card that a customer can use only with a specific merchant. Each time a payment device captures information from a closed-loop card, we store the information as a closed-loop read.
    ///
    /// Our gateway returns the following information from a closed-loop read:
    /// -	Date that the payment device captured the information.
    /// -	Unstructured payload from the card.
    /// </summary>
    /// <example><code>
    /// await client.PayrocCloud.ClosedLoopReads.RetrieveAsync(
    ///     new RetrieveClosedLoopReadsRequest { ClosedLoopReadId = "JDN4ILZB0T" }
    /// );
    /// </code></example>
    public WithRawResponseTask<ClosedLoopResponse> RetrieveAsync(
        RetrieveClosedLoopReadsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        return new WithRawResponseTask<ClosedLoopResponse>(
            RetrieveAsyncCore(request, options, cancellationToken)
        );
    }
}
