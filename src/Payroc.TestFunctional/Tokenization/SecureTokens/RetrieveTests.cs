using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestFunctional.Payments.SecureTokens;

[TestFixture, Category("Payments.Tokenization.SecureTokens")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
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
        var retrieveRequest = new RetrieveSecureTokensRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            SecureTokenId = createResponse.SecureTokenId
        };

        var retrieveResponse = await client.Tokenization.SecureTokens.RetrieveAsync(retrieveRequest);

        Assert.That(retrieveResponse.SecureTokenId, Is.EqualTo(createResponse.SecureTokenId));
    }
}
