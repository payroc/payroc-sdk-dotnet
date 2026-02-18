using Payroc.Tokenization.SecureTokens;

namespace Payroc.TestFunctional.Payments.SecureTokens;

[TestFixture, Category("Payments.Tokenization.SecureTokens")]
[Parallelizable(ParallelScope.Fixtures)]
public class PartiallyUpdateTests
{
    [Test]
    public async Task SmokeTest()
    {
        try
        {
            var client = GlobalFixture.Payments;
            var createRequest = Data.Get<TokenizationRequest>(
            [
                ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
                ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs )
            ]);
            var createResponse = await client.Tokenization.SecureTokens.CreateAsync(createRequest);
            var partiallyUpdateRequest = new PartiallyUpdateSecureTokensRequest
            {
                ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
                IdempotencyKey = Guid.NewGuid().ToString(),
                SecureTokenId = createResponse.SecureTokenId,
                Body = [
                    new PatchDocument.Add(new()
                {
                    Path = "/customer/firstName",
                    Value = "Sarah"
                }),
                new PatchDocument.Add(new()
                {
                    Path = "/customer/lastName",
                    Value = "Hopper"
                })
                ]
            };

            var updateResponse = await client.Tokenization.SecureTokens.PartiallyUpdateAsync(partiallyUpdateRequest);

            Assert.That(updateResponse.SecureTokenId, Is.EqualTo(createResponse.SecureTokenId));
        }
        catch (PayrocApiException ex)
        {
            Assert.Fail($"Exception thrown during RetrieveAsync: {ex}");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception thrown during RetrieveAsync: {ex}");
        }
    }
}
