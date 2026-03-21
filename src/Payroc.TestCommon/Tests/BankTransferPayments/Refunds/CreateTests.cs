using Payroc.BankTransferPayments.Refunds;

namespace Payroc.TestCommon.Tests.Payments.BankTransferRefunds;

[TestFixture, Category("BankTransferPayments.Refunds")]
[Parallelizable(ParallelScope.Fixtures)]
public class CreateTests
{
    [Test]
    [Ignore("No ACH-capable terminal available - need bank transfer terminal config")]
    public async Task BankTransferRefunds_Create_Success()
    {
        var client = GlobalFixture.Payments;
        var request = Data.Get<BankTransferUnreferencedRefund>(
        [
            ( i => i.IdempotencyKey, Guid.NewGuid().ToString() ),
            ( i => i.ProcessingTerminalId, GlobalFixture.TerminalIdBank ),
        ]);
        request.Order.OrderId = Guid.NewGuid().ToString().Substring(0,23);
        try
        {
            var response = await client.BankTransferPayments.Refunds.CreateAsync(request);
            Assert.That(response.RefundId, Is.Not.Null);
        }
        catch (BadRequestError ex)
        {
            Assert.Fail($"Exception occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Exception occurred: {ex.Message}");
        }
    }
}

