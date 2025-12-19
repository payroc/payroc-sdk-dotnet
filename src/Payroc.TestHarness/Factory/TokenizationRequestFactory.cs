using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestHarness.Factory;

public static class TokenizationRequestFactory
{
    public static TokenizationRequest Create(string processingTerminalId)
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            ProcessingTerminalId = "5984001",
            Source = new TokenizationRequestSource.Ach(new AchPayload()
            {
                NameOnAccount = "Test",
                AccountNumber = "123456789",
                RoutingNumber = "021000021",
            })
        };
}
