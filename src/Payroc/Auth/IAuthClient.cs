using Payroc;

namespace Payroc.Auth;

public partial interface IAuthClient
{
    /// <summary>
    /// Obtain an access token using client credentials
    /// </summary>
    WithRawResponseTask<GetTokenResponse> RetrieveTokenAsync(
        RetrieveTokenAuthRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
