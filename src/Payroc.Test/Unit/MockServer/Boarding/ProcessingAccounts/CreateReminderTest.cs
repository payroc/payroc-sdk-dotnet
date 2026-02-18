using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class CreateReminderTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "type": "pricingAgreement"
            }
            """;

        const string mockResponse = """
            {
              "type": "pricingAgreement"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/38765/reminders")
                    .WithHeader("Idempotency-Key", "8e03978e-40d5-43e8-bc93-6894a57f9324")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Boarding.ProcessingAccounts.CreateReminderAsync(
            new CreateReminderProcessingAccountsRequest
            {
                ProcessingAccountId = "38765",
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new CreateReminderProcessingAccountsRequestBody(
                    new Payroc.Boarding.ProcessingAccounts.CreateReminderProcessingAccountsRequestBody.PricingAgreement(
                        new PricingAgreementReminder()
                    )
                ),
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
