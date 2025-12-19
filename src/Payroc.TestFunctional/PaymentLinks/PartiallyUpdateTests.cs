using Payroc.PaymentLinks;
using Payroc.TestFunctional.Factories.Payments;

namespace Payroc.TestFunctional.PaymentLinks;

[TestFixture, Category("PaymentLinks")]
[Parallelizable(ParallelScope.Fixtures)]
public class PartiallyUpdateTests
{
   [Test]
   public async Task SmokeTest()
   {
       var client = GlobalFixture.Payments;
       var createRequest = Data.Get<CreatePaymentLinksRequest>([
           (i => i.ProcessingTerminalId, GlobalFixture.TerminalIdAvs),
           (i => i.IdempotencyKey, Guid.NewGuid().ToString()),
           (i => i.Body, PaymentLinksFactory.Create())
       ]);
       var createResponse = await client.PaymentLinks.CreateAsync(createRequest);
       var partiallyUpdateRequest = new PartiallyUpdatePaymentLinksRequest
       {
           IdempotencyKey = Guid.NewGuid().ToString(),
           PaymentLinkId = createResponse.AsMultiUse().PaymentLinkId ?? string.Empty,
           Body = [
               new(new PatchDocument.Add(new()
               {
                   Path = "/setupOrder/amount",
                   Value = 2999
               }))
           ]
       };
       
       var partiallyUpdateResponse = await client.PaymentLinks.PartiallyUpdateAsync(partiallyUpdateRequest);
       
       Assert.That(partiallyUpdateResponse.AsMultiUse().Status, Is.Not.Null);
   }
}
