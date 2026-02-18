using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestFunctional.Payments.SecureTokens;

[TestFixture, Category("Payments.Tokenization.SecureTokens")]
[Parallelizable(ParallelScope.Fixtures)]
public class DeleteTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<TokenizationRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        var response = await client.Tokenization.SecureTokens.CreateAsync(request);
        var deleteRequest = new DeleteSecureTokensRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            SecureTokenId = response.SecureTokenId
        };

        // There is no response body for delete, so we just ensure no exception was thrown
        Assert.DoesNotThrowAsync(async () =>
        {
            await client.Tokenization.SecureTokens.DeleteAsync(deleteRequest);
        });
    }
}
