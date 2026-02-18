using NUnit.Framework;
using Payroc;
using Payroc.Funding.FundingAccounts;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingAccounts;

[TestFixture]
public class UpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
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

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-accounts/1")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Funding.FundingAccounts.UpdateAsync(
                new UpdateFundingAccountsRequest
                {
                    FundingAccountId = 1,
                    Body = new FundingAccount
                    {
                        Type = FundingAccountType.Savings,
                        Use = FundingAccountUse.Credit,
                        NameOnAccount = "Fred Nerk",
                        PaymentMethods = new List<PaymentMethodsItem>()
                        {
                            new PaymentMethodsItem(
                                new PaymentMethodsItem.Ach(new PaymentMethodAch())
                            ),
                        },
                        Metadata = new Dictionary<string, string>()
                        {
                            { "responsiblePerson", "Jane Doe" },
                        },
                    },
                }
            )
        );
    }
}
