using Payroc.Core;
using Payroc.Tokenization.SecureTokens;
using Payroc.Tokenization.SingleUseTokens;

namespace Payroc.Tokenization;

public partial class TokenizationClient
{
    private RawClient _client;

    internal TokenizationClient(RawClient client)
    {
        try
        {
            _client = client;
            SecureTokens = new SecureTokensClient(_client);
            SingleUseTokens = new SingleUseTokensClient(_client);
        }
        catch (Exception ex)
        {
            client.Options.ExceptionHandler?.CaptureException(ex);
            throw;
        }
    }

    public SecureTokensClient SecureTokens { get; }

    public SingleUseTokensClient SingleUseTokens { get; }
}
