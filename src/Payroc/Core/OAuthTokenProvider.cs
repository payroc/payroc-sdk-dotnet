using Payroc.Auth;

namespace Payroc.Core;

public partial class OAuthTokenProvider
{
    private const double BufferInMinutes = 2;

    private string? _accessToken;

    private DateTime? _expiresAt;

    private string _apiKey;

    private AuthClient _client;

    public OAuthTokenProvider(string apiKey, AuthClient client)
    {
        _apiKey = apiKey;
        _client = client;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        if (_accessToken == null || _expiresAt == null || DateTime.UtcNow > _expiresAt)
        {
            var tokenResponse = await _client
                .GetTokenAsync(
                    new GetTokenAuthRequest { ApiKey = _apiKey }
                )
                .ConfigureAwait(false);
            _accessToken = tokenResponse.AccessToken;
            _expiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
        }
        return $"Bearer {_accessToken}";
    }
}
