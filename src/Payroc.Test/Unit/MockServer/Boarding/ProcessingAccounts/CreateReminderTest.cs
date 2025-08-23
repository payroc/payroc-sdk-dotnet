using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Boarding.ProcessingAccounts;
using Payroc.Core;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Boarding.ProcessingAccounts;

[TestFixture]
public class CreateReminderTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "type": "pricingAgreement"
            }
            """;

        const string mockResponse = """
            {
              "reminderId": "1234567",
              "type": "pricingAgreement"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/processing-accounts/processingAccountId/reminders")
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
                ProcessingAccountId = "processingAccountId",
                Body = new CreateReminderProcessingAccountsRequestBody(
                    new CreateReminderProcessingAccountsRequestBody.PricingAgreement(
                        new PricingAgreementReminder()
                    )
                ),
            }
        );
        Assert.That(
            response,
            Is.EqualTo(
                    JsonUtils.Deserialize<CreateReminderProcessingAccountsResponse>(mockResponse)
                )
                .UsingDefaults()
        );
    }
}
