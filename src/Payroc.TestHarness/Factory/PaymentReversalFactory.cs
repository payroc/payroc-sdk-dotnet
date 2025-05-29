using Payroc.Payments;

namespace Payroc.TestHarness.Factory;

public class PaymentReversalFactory
{
    public static PaymentReversal Create(string processingTerminalId = "5984001", string paymentId = "GFL9F9AXXZ")
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            PaymentId = paymentId,
            Operator = "Jane",
            Amount = 499
        };
}
