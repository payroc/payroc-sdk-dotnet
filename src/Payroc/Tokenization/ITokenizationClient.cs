using Payroc.Tokenization.SecureTokens;
using Payroc.Tokenization.SingleUseTokens;

namespace Payroc.Tokenization;

public partial interface ITokenizationClient
{
    public ISecureTokensClient SecureTokens { get; }
    public ISingleUseTokensClient SingleUseTokens { get; }
}
