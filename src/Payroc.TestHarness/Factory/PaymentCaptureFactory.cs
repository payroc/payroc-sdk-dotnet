using Payroc.Payments;

namespace Payroc.TestHarness.Factory;

public class PaymentCaptureFactory
{
    public static PaymentCapture Create(string processingTerminalId = "5984001", string paymentId = "GFL9F9AXXZ")
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            ProcessingTerminalId = processingTerminalId,
            PaymentId = paymentId,


            Operator = "Jane",
            Amount = 4999,

            Breakdown = new()
            {
                DutyAmount = 499,
                FreightAmount = 500,
                Subtotal = 4999,
                Items =
                [
                    new()
                    {
                        UnitPrice = 4000,
                        Quantity = 1
                    }
                ]
            }
        };
}
