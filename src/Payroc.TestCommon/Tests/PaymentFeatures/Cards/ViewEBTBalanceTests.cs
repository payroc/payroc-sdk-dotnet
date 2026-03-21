using Payroc.PaymentFeatures.Cards;

namespace Payroc.TestCommon.Tests.PaymentFeatures.Cards;

[TestFixture, Category("PaymentFeatures.Cards")]
[Parallelizable(ParallelScope.Fixtures)]
public class ViewEBTBalanceTests
{
    [Test]
    [Ignore("Data Errors: BadRequestError: Invalid Sharing Group: null must be 30 or less alphaNumeric characters!")]
    public async Task Cards_ViewEBTBalance_Success()
    {
        try
        {
            var client = GlobalFixture.Payments;
            var request = Data.Get<BalanceInquiry>(
                [
                    (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
                    (i => i.Currency, Currency.Usd)
                ]);

            var response = await client.PaymentFeatures.Cards.ViewEbtBalanceAsync(request);

            Assert.That(response.ResponseMessage, Is.EqualTo("Approved"));
        }
        catch (BadRequestError ex)
        {
            // Invalid Sharing Group: null must be 30 or less alphaNumeric characters!
            Assert.Fail($"Test failed due to BadRequestError: {ex.Message}");
        }
        catch (InvalidCastException ex)
        {
            Assert.Fail($"Test failed due to InvalidCastException: {ex.Message}");
        }
    }
}
