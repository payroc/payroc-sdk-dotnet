using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Payroc;
using Payroc.Core;

namespace Payroc.Auth;

public partial class AuthClient
{
    private RawClient _client;

    internal AuthClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Obtain an OAuth2 access token using client credentials
    /// </summary>
    /// <example><code>
    /// await client.Auth.GetTokenAsync(
    ///     new GetTokenAuthRequest { ClientId = "client_id", ClientSecret = "client_secret" }
    /// );
    /// </code></example>
    public async Task<GetTokenResponse> GetTokenAsync(
        GetTokenAuthRequest request,
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
                            BaseUrl = _client.Options.Environment.Identity,
                            Method = HttpMethod.Post,
                            Path = "authorize",
                            ContentType = "application/json",
                            Options = options,
                            Headers = { { "x-api-key", request.ApiKey } }
                        },
                        cancellationToken
                    )
                    .ConfigureAwait(false);
                if (response.StatusCode is >= 200 and < 400)
                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonUtils.Deserialize<GetTokenResponse>(responseBody)!;
                    }
                    catch (JsonException e)
                    {
                        throw new PayrocException("Failed to deserialize response", e);
                    }
                }

                {
                    var responseBody = await response.Raw.Content.ReadAsStringAsync();
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
