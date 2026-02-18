using Payroc;

namespace Payroc.Tokenization.SingleUseTokens;

public partial interface ISingleUseTokensClient
{
    /// <summary>
    /// Use this method to create a single-use token that represents a customer’s payment details.
    ///
    /// A single-use token expires after 30 minutes and merchants can use them only once.
    ///
    /// **Note:** To create a reusable permanent token, go to [Create Secure Token](https://docs.payroc.com/api/schema/tokenization/secure-tokens/create).
    ///
    /// In the request, send the customer’s payment details. If the request is successful, our gateway returns a token that you can use in a follow-on action, for example, [run a sale](https://docs.payroc.com/api/schema/card-payments/payments/create).
    /// </summary>
    WithRawResponseTask<SingleUseToken> CreateAsync(
        SingleUseTokenRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    );
}
