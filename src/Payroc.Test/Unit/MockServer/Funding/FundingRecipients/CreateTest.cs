using global::System.Threading.Tasks;
using NUnit.Framework;
using Payroc;
using Payroc.Core;
using Payroc.Funding.FundingRecipients;
using Payroc.Test.Unit.MockServer;

namespace Payroc.Test.Unit.MockServer.Funding.FundingRecipients;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "recipientType": "privateCorporation",
              "taxId": "12-3456789",
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
              ],
              "owners": [
                {
                  "firstName": "Jane",
                  "lastName": "Doe",
                  "dateOfBirth": "1964-03-22",
                  "address": {
                    "address1": "1 Example Ave.",
                    "city": "Chicago",
                    "state": "Illinois",
                    "country": "US",
                    "postalCode": "60056"
                  },
                  "identifiers": [
                    {
                      "type": "nationalId",
                      "value": "xxxxx4320"
                    }
                  ],
                  "contactMethods": [
                    {
                      "value": "jane.doe@example.com",
                      "type": "email"
                    }
                  ],
                  "relationship": {
                    "isControlProng": true
                  }
                }
              ],
              "fundingAccounts": [
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
              ]
            }
            """;

        const string mockResponse = """
            {
              "recipientId": 234,
              "status": "approved",
              "createdDate": "2024-07-02T15:30:00.000Z",
              "lastModifiedDate": "2024-07-02T15:30:00.000Z",
              "recipientType": "privateCorporation",
              "taxId": "12-3456789",
              "charityId": "charityId",
              "doingBusinessAs": "doingBusinessAs",
              "address": {
                "address1": "1 Example Ave.",
                "address2": "Example Address Line 2",
                "address3": "Example Address Line 3",
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
              ],
              "metadata": {
                "yourCustomField": "abc123"
              },
              "owners": [
                {
                  "ownerId": 4564,
                  "link": {
                    "rel": "owner",
                    "href": "https://api.payroc.com/v1/owners/4564",
                    "method": "get"
                  }
                }
              ],
              "fundingAccounts": [
                {
                  "fundingAccountId": 123,
                  "status": "approved",
                  "link": {
                    "rel": "fundingAccount",
                    "href": "https://api.payroc.com/v1/funding-accounts/123",
                    "method": "get"
                  }
                },
                {
                  "fundingAccountId": 124,
                  "status": "rejected",
                  "link": {
                    "rel": "fundingAccount",
                    "href": "https://api.payroc.com/v1/funding-accounts/124",
                    "method": "get"
                  }
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/funding-recipients")
                    .WithHeader("Idempotency-Key", "Idempotency-Key")
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

        var response = await Client.Funding.FundingRecipients.CreateAsync(
            new CreateFundingRecipient
            {
                IdempotencyKey = "Idempotency-Key",
                RecipientType = CreateFundingRecipientRecipientType.PrivateCorporation,
                TaxId = "12-3456789",
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
                Owners = new List<Owner>()
                {
                    new Owner
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        DateOfBirth = new DateOnly(1964, 3, 22),
                        Address = new Address
                        {
                            Address1 = "1 Example Ave.",
                            City = "Chicago",
                            State = "Illinois",
                            Country = "US",
                            PostalCode = "60056",
                        },
                        Identifiers = new List<Identifier>()
                        {
                            new Identifier { Type = "nationalId", Value = "xxxxx4320" },
                        },
                        ContactMethods = new List<ContactMethod>()
                        {
                            new ContactMethod(
                                new ContactMethod.Email(
                                    new ContactMethodEmail { Value = "jane.doe@example.com" }
                                )
                            ),
                        },
                        Relationship = new OwnerRelationship { IsControlProng = true },
                    },
                },
                FundingAccounts = new List<FundingAccount>()
                {
                    new FundingAccount
                    {
                        Type = FundingAccountType.Checking,
                        Use = FundingAccountUse.Credit,
                        NameOnAccount = "Jane Doe",
                        PaymentMethods = new List<PaymentMethodsItem>()
                        {
                            new PaymentMethodsItem(
                                new PaymentMethodsItem.Ach(new PaymentMethodAch())
                            ),
                        },
                    },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<FundingRecipient>(mockResponse)).UsingDefaults()
        );
    }
}
