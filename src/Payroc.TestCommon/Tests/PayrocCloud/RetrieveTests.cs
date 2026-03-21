using Payroc.PayrocCloud.Signatures;

namespace Payroc.TestCommon.Tests.PayrocCloud;

[TestFixture, Category("PayrocCloud")]
[Parallelizable(ParallelScope.Fixtures)]
public class RetrieveTests
{
    [Test]
    [Ignore("Data Errors: NotFoundError for existing SignatureId")]
    public async Task PayrocCloud_Retrieve_Success()
    {
        var client = GlobalFixture.Payments;
        var request = new RetrieveSignaturesRequest
        {
            SignatureId = "JDN4ILZB0T"
        };

        try
        {
            var response = await client.PayrocCloud.Signatures.RetrieveAsync(request);
            Assert.That(response.SignatureId, Is.Not.Null);
        }
        catch (BadRequestError e)
        {
            Assert.Fail($"Exception occurred: {e.Message}");
        }
        catch (NotFoundError e)
        {
            Assert.Fail($"Exception occurred: {e.Message}");
        }
    }
}
