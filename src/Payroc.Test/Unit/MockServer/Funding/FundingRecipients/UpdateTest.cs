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
              "taxId": "12-3456789",
              "doingBusinessAs": "Doe Hot Dogs",
              "address": {
                "address1": "2 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
                "city": "Chicago",
                "state": "Illinois",
                "country": "US",
                "postalCode": "60056"
              },
              "contactMethods": [
                {
                  "type": "email",
                  "value": "jane.doe@example.com"
                },
                {
                  "type": "phone",
                  "value": "2025550164"
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
                        TaxId = "12-3456789",
                        DoingBusinessAs = "Doe Hot Dogs",
                        Address = new Address
                        {
                            Address1 = "2 Example Ave.",
                            Address2 = "Example Address Line 2",
                            Address3 = "Example Address Line 3",
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
                            new ContactMethod(
                                new ContactMethod.Phone(
                                    new ContactMethodPhone { Value = "2025550164" }
                                )
                            ),
                        },
                        Metadata = new Dictionary<string, string>()
                        {
                            { "responsiblePerson", "Jane Doe" },
                        },
                        Owners = new List<FundingRecipientOwnersItem>()
                        {
                            new FundingRecipientOwnersItem
                            {
                                OwnerId = 12346,
                                Link = new FundingRecipientOwnersItemLink
                                {
                                    Rel = "owner",
                                    Href = "https://api.payroc.com/v1/owners/12346",
                                    Method = "get",
                                },
                            },
                        },
                        FundingAccounts = new List<FundingRecipientFundingAccountsItem>()
                        {
                            new FundingRecipientFundingAccountsItem
                            {
                                FundingAccountId = 124,
                                Status = FundingRecipientFundingAccountsItemStatus.Approved,
                                Link = new FundingRecipientFundingAccountsItemLink
                                {
                                    Rel = "fundingAccount",
                                    Href = "https://api.payroc.com/v1/funding-accounts/124",
                                    Method = "get",
                                },
                            },
                        },
                    },
                }
            )
        );
    }
}
