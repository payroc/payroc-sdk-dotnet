using Payroc.Tokenization.SingleUseTokens;

namespace Payroc.TestFunctional.Payments.SingleUseTokens;

[TestFixture, Category("Payments.SingleUseTokens")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<SingleUseTokenRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
            ]);

        var response = await client.Tokenization.SingleUseTokens.CreateAsync(request);

        Assert.That(response.Token, Is.Not.Null);
    }
}
