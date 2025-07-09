using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class CreateAccountTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "type": "checking",
              "use": "credit",
              "nameOnAccount": "Jane Doe",
              "paymentMethods": [
                {
                  "type": "ach"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "fundingAccountId": 123,
              "createdDate": "2024-07-02T15:30:00.000Z",
              "lastModifiedDate": "2024-07-02T15:30:00.000Z",
              "status": "approved",
              "type": "checking",
              "use": "credit",
              "nameOnAccount": "Jane Doe",
              "paymentMethods": [
                {
                  "value": {
                    "routingNumber": "123456789",
                    "accountNumber": "1234567890"
                  },
                  "type": "ach"
                }
              ],
              "metadata": {
                "yourCustomField": "abc123"
              },
              "links": [
                {
                  "rel": "parent",
                  "method": "get",
                  "href": "https://api.payroc.com/v1/funding-recipient/234"
                }
              ]
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
                    Type = FundingAccountType.Checking,
                    Use = FundingAccountUse.Credit,
                    NameOnAccount = "Jane Doe",
                    PaymentMethods = new List<PaymentMethodsItem>()
                    {
                        new PaymentMethodsItem(new PaymentMethodsItem.Ach(new PaymentMethodAch())),
                    },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<FundingAccount>(mockResponse)).UsingDefaults()
        );
    }
}
