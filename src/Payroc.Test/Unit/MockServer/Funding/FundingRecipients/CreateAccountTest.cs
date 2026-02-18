using NUnit.Framework;
using Payroc;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;
using Payroc.Test.Utils;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class CreateAccountTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "type": "savings",
              "use": "credit",
              "nameOnAccount": "Fred Nerk",
              "paymentMethods": [
                {
                  "type": "ach"
                }
              ],
              "metadata": {
                "responsiblePerson": "Jane Doe"
              }
            }
            """;

        const string mockResponse = """
            {
              "type": "checking",
              "use": "credit",
              "nameOnAccount": "Jane Doe",
              "paymentMethods": [
                {
                  "type": "ach",
                  "value": {
                    "routingNumber": "123456789",
                    "accountNumber": "1234567890"
                  }
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients/1/funding-accounts")
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

        var response = await Client.Funding.FundingRecipients.CreateAccountAsync(
            new CreateAccountFundingRecipientsRequest
            {
                RecipientId = 1,
                IdempotencyKey = "8e03978e-40d5-43e8-bc93-6894a57f9324",
                Body = new FundingAccount
                {
                    Type = FundingAccountType.Savings,
                    Use = FundingAccountUse.Credit,
                    NameOnAccount = "Fred Nerk",
                    PaymentMethods = new List<PaymentMethodsItem>()
                    {
                        new PaymentMethodsItem(new PaymentMethodsItem.Ach(new PaymentMethodAch())),
                    },
                    Metadata = new Dictionary<string, string>()
                    {
                        { "responsiblePerson", "Jane Doe" },
                    },
                },
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
