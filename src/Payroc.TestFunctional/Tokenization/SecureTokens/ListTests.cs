using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestFunctional.Payments.SecureTokens;

[TestFixture, Category("Payments.Tokenization.SecureTokens")]
[Parallelizable(ParallelScope.Fixtures)]
public class ListTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var createRequest =Data.Get<TokenizationRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);
        _ = await client.Tokenization.SecureTokens.CreateAsync(createRequest);
        _ = await client.Tokenization.SecureTokens.CreateAsync(createRequest);
        var request = new ListSecureTokensRequest
        {
            ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
            SecureTokenId = createRequest.SecureTokenId,
        };

        var response = await client.Tokenization.SecureTokens.ListAsync(request);

        Assert.That(response.CurrentPage.Count(), Is.GreaterThan(0));
        Assert.That(response.HasNextPage, Is.True);
    }
}
