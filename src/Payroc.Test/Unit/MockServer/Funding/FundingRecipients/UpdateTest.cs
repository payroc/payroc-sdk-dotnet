using NUnit.Framework;
using Payroc;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class UpdateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "recipientType": "privateCorporation",
              "taxId": "123456789",
              "doingBusinessAs": "doingBusinessAs",
              "address": {
                "address1": "1 Example Ave.",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "contactMethods": [
                {
                  "value": "jane.doe@example.com",
                  "type": "email"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients/1")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPut()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Funding.FundingRecipients.UpdateAsync(
                new UpdateFundingRecipientsRequest
                {
                    RecipientId = 1,
                    Body = new FundingRecipient
                    {
                        RecipientType = FundingRecipientRecipientType.PrivateCorporation,
                        TaxId = "123456789",
                        DoingBusinessAs = "doingBusinessAs",
                        Address = new Address
                        {
                            Address1 = "1 Example Ave.",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                        ContactMethods = new List<ContactMethod>()
                        {
                            new ContactMethod(
                                new ContactMethod.Email(
                                    new ContactMethodEmail { Value = "jane.doe@example.com" }
                                )
                            ),
                        },
                    },
                }
            )
        );
    }
}
