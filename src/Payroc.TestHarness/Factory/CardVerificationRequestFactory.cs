using Payroc.PaymentFeatures.Cards;

namespace Payroc.TestHarness.Factory;

public class CardVerificationRequestFactory
{
    public static CardVerificationRequest Create(string processingTerminalId = "5984001")
        => new()
        {
            IdempotencyKey = Guid.NewGuid().ToString(),
            ProcessingTerminalId = processingTerminalId,
            Card = new(new CardVerificationRequestCard.Card(CardPayloadFactory.Create()))
        };
}
