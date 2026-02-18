using Payroc.Tokenization.SecureTokens;
using Payroc.Tokenization.SingleUseTokens;

namespace Payroc.TestFunctional.Payments.SecureTokens;

[TestFixture, Category("Payments.Tokenization.SecureTokens")]
[Parallelizable(ParallelScope.Fixtures)]
public class UpdateAccountTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRequest = Data.Get<TokenizationRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        var createResponse = await client.Tokenization.SecureTokens.CreateAsync(createRequest);
        var singleUseTokenRequest = Data.Get<SingleUseTokenRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        var singleUseTokenResponse = await client.Tokenization.SingleUseTokens.CreateAsync(singleUseTokenRequest);
        var updateRequest = new UpdateAccountSecureTokensRequest
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            SecureTokenId = createResponse.SecureTokenId,
            Body = new AccountUpdate(new SingleUseTokenAccountUpdate
            {
                Token = singleUseTokenResponse.Token ?? string.Empty
            })
        };

        var updateResponse = await client.Tokenization.SecureTokens.UpdateAccountAsync(updateRequest);
            
        Assert.That(updateResponse.SecureTokenId, Is.EqualTo(createResponse.SecureTokenId));
    }
}
