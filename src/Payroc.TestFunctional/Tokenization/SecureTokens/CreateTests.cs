using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestFunctional.Payments.SecureTokens;

[TestFixture, Category("Payments.SecureTokens")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<TokenizationRequest>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
        ]);;

        var response = await client.Tokenization.SecureTokens.CreateAsync(request);

        Assert.That(response, Is.Not.Null);
        Assert.That(response.Token, Is.Not.Null);
        Assert.That(response.SecureTokenId, Is.Not.Null);
        Assert.That(response.SecureTokenId, Is.Not.Empty);
        Assert.That(response.Status, Is.EqualTo("cvvValidated"));
    }
}
