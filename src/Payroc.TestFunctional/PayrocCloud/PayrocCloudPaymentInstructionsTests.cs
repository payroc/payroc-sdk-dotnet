using Payroc.PayrocCloud.PaymentInstructions;

namespace Payroc.TestFunctional.PayrocCloud;

[TestFixture, Category("PayrocCloud")]
[Parallelizable(ParallelScope.Fixtures)]
public class PayrocCloudPaymentInstructionsTests
{
    private static string NewKey() => Guid.NewGuid().ToString();

    [Test]
    [Ignore("PayrocCloud")]
    public async Task SmokeTest()
    {
        var client = GlobalFixture.Payments;

        var submissionResponse = await client.PayrocCloud.PaymentInstructions.SubmitAsync(
            new PaymentInstructionRequest
            {
                SerialNumber = "1850010868",
                IdempotencyKey = NewKey(),
                Operator = "Jane",
                ProcessingTerminalId = GlobalFixture.TerminalIdAvs,
                Order = new PaymentInstructionOrder
                {
                    OrderId = "OrderRef6543",
                    Amount = 4999,
                    Currency = Currency.Usd,
                },
                CustomizationOptions = new CustomizationOptions
                {
                    EntryMethod = CustomizationOptionsEntryMethod.DeviceRead,
                },
                AutoCapture = true,
            }
        );

        var retrievalResponse = await client.PayrocCloud.PaymentInstructions.RetrieveAsync(
            new RetrievePaymentInstructionsRequest
            {
                PaymentInstructionId = submissionResponse.PaymentInstructionId!
            }
        );

        Assert.DoesNotThrowAsync(async () =>
            await client.PayrocCloud.PaymentInstructions.DeleteAsync(
                new DeletePaymentInstructionsRequest
                {
                    PaymentInstructionId = submissionResponse.PaymentInstructionId!
                }));

        Assert.Multiple(() =>
        {
            Assert.That(submissionResponse.Status, Is.Not.Null);
            Assert.That(submissionResponse.Status, Is.EqualTo(DeviceInstructionStatus.Completed));
            Assert.That(retrievalResponse.Status, Is.Not.Null);
            Assert.That(retrievalResponse.Status, Is.EqualTo(DeviceInstructionStatus.Completed));
        });
    }
}
